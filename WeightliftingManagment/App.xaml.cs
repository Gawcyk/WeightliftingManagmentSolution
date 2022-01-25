using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using MahApps.Metro.Controls;

using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models.Config;
using WeightliftingManagment.FlyoutControlManager;
using WeightliftingManagment.Helpers;
using WeightliftingManagment.Localization.LocalizationModel;
using WeightliftingManagment.Moduls.Dialogs;
using WeightliftingManagment.Moduls.Judge;
using WeightliftingManagment.Moduls.ParticipantBodyWeight;
using WeightliftingManagment.Moduls.Settings;
using WeightliftingManagment.Views;

namespace WeightliftingManagment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private string[]? _startUpArgs;
        protected override Window CreateShell() => Container.Resolve<ShellWindow>();

        protected override void OnStartup(StartupEventArgs e)
        {
            _startUpArgs = e.Args;
            base.OnStartup(e);
        }

        protected override async void OnInitialized()
        {
            var restoreService = Container.Resolve<IPersistAndRestoreService>();
            await restoreService.RestoreDataAsync().ConfigureAwait(true);

            var restoreThemeService = Container.Resolve<IPersistAndRestoreThemeService>();
            await restoreThemeService.RestoreDataAsync().ConfigureAwait(true);

            var restoreSinclaireService = Container.Resolve<IPersistAndRestoreSinclaireCoefficientService>();
            await restoreSinclaireService.RestoreDataAsync().ConfigureAwait(true);

            var themeConfig = Container.Resolve<ThemeConfig>();
            var themeService = Container.Resolve<IThemeSelectorService>();
            themeService.UpdateInternalConfig(themeConfig);


            var appConfig = Container.Resolve<AppConfig>();
            var localization = Container.Resolve<LocalizationService>();
            localization.RegisterLang(appConfig.LanguageFolder, appConfig.LanguageFileName);
            localization.ChangeCulture(CultureInfo.InstalledUICulture);

            await Task.CompletedTask;
            base.OnInitialized();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SettingsModule>();
            moduleCatalog.AddModule<DialogsModule>();
            moduleCatalog.AddModule<ParticipantBodyWeightModule>();
            moduleCatalog.AddModule<JudgeModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping<FlyoutsControl>(Container.Resolve<FlyoutsControlRegionAdapter>());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterAllType();

            // Configuration
            var configuration = BuildConfiguration();
            var appConfig = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();
            var themeConfig = configuration.GetSection(nameof(ThemeConfig)).Get<ThemeConfig>();
            var sinclaireConfig = configuration.GetSection(nameof(SinclaireConfig)).Get<SinclaireConfig>();

            containerRegistry.RegisterInstance(configuration);
            containerRegistry.RegisterInstance(appConfig);
            containerRegistry.RegisterInstance(themeConfig);
            containerRegistry.RegisterInstance(sinclaireConfig);
        }

        private IConfiguration BuildConfiguration()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("AppSettings.json")
                .AddCommandLine(_startUpArgs)
                .Build();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) => e.Handled = true;

        private async void OnExitAsync(object sender, ExitEventArgs e)
        {
            var persistService = Container.Resolve<IPersistAndRestoreService>();
            await persistService.PersistDataAsync().ConfigureAwait(true);

            var persistThemeService = Container.Resolve<IPersistAndRestoreThemeService>();
            await persistThemeService.PersistDataAsync().ConfigureAwait(true);

            var persistSinclaireService = Container.Resolve<IPersistAndRestoreSinclaireCoefficientService>();
            await persistSinclaireService.PersistDataAsync().ConfigureAwait(true);

        }
    }
}