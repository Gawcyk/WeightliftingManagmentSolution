using System.Globalization;
using System.Windows;

using MahApps.Metro.Controls;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using WeightliftingManagment.FlyoutControlManager;
using WeightliftingManagment.Helpers;
using WeightliftingManagment.Localization.LocalizationModel;
using WeightliftingManagment.Localization.Readers;
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
        protected override Window CreateShell() => Container.Resolve<ShellWindow>();

        protected override async void OnInitialized()
        {
            //var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            //persistAndRestoreService.RestoreData();

            var localization = Container.Resolve<LocalizationService>();
            localization.RegisterLang($"{Environment.CurrentDirectory}/Lang");
            localization.ChangeCulture(CultureInfo.InstalledUICulture);

            base.OnInitialized();
            await Task.CompletedTask;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SettingsModule>();
            moduleCatalog.AddModule<ParticipantBodyWeightModule>();
            moduleCatalog.AddModule<JudgeModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping<FlyoutsControl>(Container.Resolve<FlyoutsControlRegionAdapter>());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry.RegisterCoreServices().RegisterAppServices().RegisterViewAndViewModel();
    }
}