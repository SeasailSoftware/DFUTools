using Caliburn.Micro;
using ControlzEx.Theming;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace STM32FirmwareUpdater.ViewModels.Settings
{
    public class ThemeChangedMessage
    {
        public SolidColorBrush Brush { get; set; }

        public ThemeChangedMessage(SolidColorBrush brush)
        {
            Brush = brush;
        }
    }
    class ThemeSettingViewModel : ViewModelBase
    {

        private LocalConfig _localConfig => IoC.Get<LocalConfig>();
        public ThemeSettingViewModel()
        {
            DisplayName = Translater.Trans("s_Theme");
            Themes = ThemeManager.Current.Themes.Where(x => x.Name.Contains("Light")).ToList();
            CurrentTheme = Themes.FirstOrDefault(x => x.Name == _localConfig.Theme);
        }

        public List<Theme> Themes { get; set; }

        private Theme? _currentTheme;
        public Theme? CurrentTheme
        {
            get => _currentTheme;
            set
            {
                _currentTheme = value;
                NotifyOfPropertyChange(() => CurrentTheme);
                if (value != null)
                {
                    _localConfig.Theme = value.Name;
                    ThemeManager.Current.ChangeTheme(Application.Current, value);
                }
            }
        }
    }
}
