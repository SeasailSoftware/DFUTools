using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using STM32FirmwareUpdater.Core;
using STM32FirmwareUpdater.i18N;
using STM32FirmwareUpdater.Models;
using STM32FirmwareUpdater.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STM32FirmwareUpdater.ViewModels
{
    [Export(typeof(IShell))]
    internal class ShellViewModel : ViewModelBase
    {
        private const string DfuPath = "DFUTools\\DFU.exe";
        private string? _dfuFilePath;

        private Process? _listProcess;
        private Process? _upgradeProcess;
        private Process? _generateDfuProcess;
        public IEventAggregator EventAggregator => IoC.Get<IEventAggregator>();
        public ObservableCollection<DfuDeviceInfo> DfuDevices { get; set; } = new ObservableCollection<DfuDeviceInfo>();

        private DfuDeviceInfo? _currentDevice;
        public DfuDeviceInfo? CurrentDevice
        {
            get => _currentDevice;
            set
            {
                _currentDevice = value;
                NotifyOfPropertyChange(() => CurrentDevice);
            }
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                NotifyOfPropertyChange(() => Progress);
            }
        }

        private string? _progressText;
        public string? ProgressText
        {
            get => _progressText;
            set
            {
                _progressText = value;
                NotifyOfPropertyChange(() => ProgressText);
            }
        }
        public string? Version => $"v{Assembly.GetExecutingAssembly()?.GetName().Version}";
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            EventAggregator.SubscribeOnUIThread(this);
            _listProcess = new Process
            {
                StartInfo =
                {
                    FileName = DfuPath,
                    Arguments = "-l",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };
            _generateDfuProcess = new Process
            {
                StartInfo =
                {
                    FileName = DfuPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };

            _upgradeProcess = new Process
            {
                StartInfo =
                {
                    FileName = DfuPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                }
            };
            _upgradeProcess.EnableRaisingEvents = true;
            _upgradeProcess.Exited += (sender, args) => EventAggregator.PublishOnUIThreadAsync(new DfuProcessExitedMessage(sender));
            _upgradeProcess.OutputDataReceived += OnDfuOutputReceived;
            return base.OnInitializeAsync(cancellationToken);
        }

        private void OnDfuOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                var match = Regex.Match(e.Data, @"(\d+)%");
                if (match.Success)
                {
                    OnUIThread(() =>
                    {
                        var progress = int.Parse(match.Groups[1].Value);
                        Progress = progress; 
                        ProgressText = Translater.Trans(e.Data.StartsWith("Erase") ? "s_Erasing$0$Percentage" : "s_Upgrading$0$Percentage", progress);
                    });

                    //_progressDialogController.SetMessage(Translater.Trans(e.Data.StartsWith("Erase") ? "s_Erasing$0$Percentage" : "s_Upgrading$0$Percentage", progress));
                    //_progressDialogController.SetProgress(progress);
                }
            }
        }

        private string? _firmwarePath;
        public string? FirmwarePath
        {
            get => _firmwarePath;
            set
            {
                if (value == _firmwarePath) return;
                _firmwarePath = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                NotifyOfPropertyChange(() => IsRunning);
            }
        }

        private bool _upgrading;
        public bool Upgrading
        {
            get => _upgrading;
            set
            {
                _upgrading = value;
                NotifyOfPropertyChange(() => Upgrading);
            }
        }

        #region Commands

        public RelayCommand LanguageSettingCommand => new RelayCommand(async x =>
        {
            var model = new LanguageSettingViewModel();
            await IoC.Get<IWindowManager>().ShowDialogAsync(model);
        });

        public RelayCommand ThemeSettingCommand => new RelayCommand(async x =>
        {
            var model = new ThemeSettingViewModel();
            await IoC.Get<IWindowManager>().ShowDialogAsync(model);
        });

        public RelayCommand BrowseFirmwareFileCommand => new RelayCommand(x =>
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "固件程序(*.dfu;*.hex;*.bin)|*.dfu;*.hex;*.bin";
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                FirmwarePath = dialog.FileName;

                var extension = System.IO.Path.GetExtension(FirmwarePath).ToLower();
                if (extension == ".bin" || extension == ".hex")
                {
                    _dfuFilePath = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName) + ".dfu";
                    _dfuFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), _dfuFilePath);
                    if (!GenerateDfuFile(FirmwarePath, _dfuFilePath))
                    {
                        _dfuFilePath = null;
                    }
                }
                else
                {
                    _dfuFilePath = FirmwarePath;
                }
            }
        });

        public RelayCommand RefreshCommand => new RelayCommand(async x =>
        {
            await Task.Run(() =>
            {

                try
                {
                    OnUIThread(() =>
                    {
                        IsRunning = true;
                        DfuDevices.Clear();
                    });

                    _listProcess?.Start();
                    _listProcess?.WaitForExit(10000);

                    OnUIThread(() =>
                    {
                        var list = _listProcess?.StandardOutput.ReadToEnd().Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        _listProcess?.Close();
                        if (list != null)
                        {
                            foreach (var s in list)
                            {
                                var match = Regex.Match(s, @"(\d+)\.\r\n\s*NAME:\s*([\w\s\d]+)\r\n\s*PATH:\s*(\S+)");
                                if (match.Success)
                                {
                                    DfuDevices.Add(new DfuDeviceInfo
                                    {
                                        ID = int.Parse(match.Groups[1].Value),
                                        Description = match.Groups[2].Value,
                                        Path = match.Groups[3].Value
                                    });
                                }
                            }
                        }
                    });
                }
                catch (Exception e)
                {
                    return;
                }
                finally
                {
                    OnUIThread(() =>
                    {
                        IsRunning = false;
                        CommandManager.InvalidateRequerySuggested();
                    });
                }
            });
        }, y => !IsRunning);

        public RelayCommand UpgradeCommand => new RelayCommand(async x =>
        {
            Upgrading = true;
            await Task.Run(() =>
            {
                try
                {
                    if (_upgradeProcess == null) return;
                    _upgradeProcess.StartInfo.Arguments = $"--upgrade \"{_dfuFilePath}\" {DfuDevices.IndexOf(CurrentDevice) + 1}";
                    if (_upgradeProcess.Start())
                    {
                        _upgradeProcess.WaitForExit(50);
                        if (!_upgradeProcess.HasExited)
                        {
                            _upgradeProcess.BeginOutputReadLine();
                            while (!_upgradeProcess.HasExited)
                            {
                                Thread.Sleep(1000);
                            }
                            if (_upgradeProcess.ExitCode == 0)
                            {

                            }
                            _upgradeProcess.CancelOutputRead();
                            _upgradeProcess.Close();
                            _upgradeProcess.Kill();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {

                    OnUIThread(() =>
                    {
                        Upgrading = false;
                        CommandManager.InvalidateRequerySuggested();
                    });
                }
            });
        }, y => CurrentDevice != null && !Upgrading);
        #endregion

        private bool GenerateDfuFile(string input, string output)
        {
            if (System.IO.File.Exists(output))
            {
                System.IO.File.Delete(output);
            }

            if (_generateDfuProcess == null) return false;
            _generateDfuProcess.StartInfo.Arguments = $"-g \"{output}\" \"{input}\"";
            _generateDfuProcess.Start();
            try
            {
                _generateDfuProcess.Start();
                _generateDfuProcess.WaitForExit();
                Debug.WriteLine(_generateDfuProcess.StandardOutput.ReadToEnd());
                int exitCode = _generateDfuProcess.ExitCode;
                bool isOk = (exitCode == 0);
                if (exitCode == -7)
                {
                    if (System.IO.File.Exists(output))
                    {
                        isOk = true;
                    }
                }
                _generateDfuProcess.Close();

                return isOk;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
