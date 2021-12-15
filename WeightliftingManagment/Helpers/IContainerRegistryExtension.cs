
using Prism.Ioc;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Services;
using WeightliftingManagment.Localization.LocalizationModel;
using WeightliftingManagment.Services;
using WeightliftingManagment.ViewModels;
using WeightliftingManagment.Views;

namespace WeightliftingManagment.Helpers
{
    public static class IContainerRegistryExtension
    {
        public static IContainerRegistry RegisterCoreServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFileService, FileService>();
            return containerRegistry;
        }

        public static IContainerRegistry RegisterAppServices(this IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
            //containerRegistry.Register<ISystemService, SystemService>();
            //containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            containerRegistry.RegisterSingleton<IThemeSelectorService, ThemeSelectorService>();
            containerRegistry.RegisterSingleton<IFlyoutService, FlyoutService>();
            containerRegistry.RegisterInstance(typeof(LocalizationService), LocalizationService.Instance);
            return containerRegistry;
        }

        public static IContainerRegistry RegisterViewAndViewModel(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ShellWindow, ShellWindowViewModel>();
            return containerRegistry;
        }

    }
}
