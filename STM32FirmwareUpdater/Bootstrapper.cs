using Caliburn.Micro;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using STM32FirmwareUpdater.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;
using STM32FirmwareUpdater.i18N;

namespace STM32FirmwareUpdater
{
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer _container;
        private ITranslater _translater;
        private LocalConfig _localConfig;
        //初始化
        public Bootstrapper()
        {
            Initialize();
            Application.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        //重写Configure
        protected override void Configure()
        {
            var aggregateCatalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x))
     .OfType<ComposablePartCatalog>());

            _container = new CompositionContainer(aggregateCatalog);
            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            //初始化配置文件
            InitializeConfig();
            batch.AddExportedValue(_localConfig);
            //batch.AddExportedValue<AppSettings>(_config);
            //batch.AddExportedValue<DeviceService>(new DeviceService());
            //batch.AddExportedValue<Seasail.Core.Control.Views.Dialog.MessageBoxView>
            //初始化语言
            InitializeCulture();
            batch.AddExportedValue(_translater);
            _container.Compose(batch);
        }

        private void InitialzeDataAccess(CompositionBatch batch)
        {

        }

        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);
            return exports.First();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(
                AttributedModelServices.GetContractName(service));
        }


        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            //加载启动动画
            LoadSplashScreen();

            // 自定义视图、视图模型查找
            ViewLocator.LocateTypeForModelType = LocateTypeForModelType;

            // 初始化自定义的值替换
            InitSpecialValues();

            // 解决控件时间显示不是本地格式的问题
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            // 初始化显示主题
            InitializeTheme();

            //设置显示主界面
            await DisplayRootViewForAsync<IShell>();
            Application.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }



        /// <summary>
        /// 加载开机界面
        /// </summary>
        private void LoadSplashScreen()
        {
            ////在资源文件中定义了SplashScreen，不再需要手动启动开机动画
            //string splashScreenPngPath = "Resources/SplashScreen.png";
            //SplashScreen s = new SplashScreen(splashScreenPngPath);
            //s.Show(true,true);
            //s.Close(TimeSpan.FromSeconds(3));
        }
        private void InitializeTheme()
        {
            Theme theme = ThemeManager.Current.Themes.FirstOrDefault(p => p.Name == _localConfig.Theme);
            if (theme != null && theme != ThemeManager.Current.DetectTheme())
                ThemeManager.Current.ChangeTheme(Application.Current, theme.Name);
        }

        /// <summary>
        /// 在这里添加我自已的Caliburn.Micro绑定变量
        /// </summary>
        private void InitSpecialValues()
        {
            //MessageBinder.SpecialValues.Add("$clickedItem",
            //    c => (c.EventArgs as ItemClickEventArgs)?.ClickedItem);
        }

        // 定位视图类型，支持派生类继承父视图
        private static Type LocateTypeForModelType(Type modelType, DependencyObject displayLocation, object context)
        {
            var viewTypeName = modelType.FullName;

            if (Caliburn.Micro.View.InDesignMode)
            {
                viewTypeName = ViewLocator.ModifyModelTypeAtDesignTime(viewTypeName);
            }

            viewTypeName = viewTypeName.Substring(0, viewTypeName.IndexOf('`') < 0
                ? viewTypeName.Length
                : viewTypeName.IndexOf('`'));

            var viewTypeList = ViewLocator.TransformName(viewTypeName, context);
            var viewType = AssemblySource.FindTypeByNames(viewTypeList);
            if (viewType == null)
            {
                Trace.TraceWarning("View not found. Searched: {0}.", string.Join(", ", viewTypeList.ToArray()));

                if (modelType.BaseType != null)
                {
                    return ViewLocator.LocateTypeForModelType(modelType.BaseType, displayLocation, context);
                }
            }

            return viewType;
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
        private void InitializeCulture()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            if (!string.IsNullOrEmpty(_localConfig.Culture))
            {
                ci = CultureInfo.GetCultureInfo(_localConfig.Culture);
            }
            Utils.LocalUtil.SwitchCulture(ci);
            _translater = new Translater();
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void InitializeConfig()
        {
            _localConfig = LocalConfig.Load();
            if (_localConfig == null)
                _localConfig = new LocalConfig();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            _localConfig.Save();
            base.OnExit(sender, e);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Application.Current.Shutdown(-1);
        }
    }
}
