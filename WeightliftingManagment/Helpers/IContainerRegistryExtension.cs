
using System.Configuration;
using System.Reflection;

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
        public static IContainerRegistry RegisterAllType(this IContainerRegistry containerRegistry) 
            => containerRegistry
            .RegisterCoreServices()
            .RegisterAppServices()
            .RegisterViewAndViewModel();

        public static IContainerRegistry RegisterCoreServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFileService, FileService>();
            return containerRegistry;
        }

        public static IContainerRegistry RegisterAppServices(this IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
            //containerRegistry.Register<ISystemService, SystemService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            containerRegistry.Register<IPersistAndRestoreThemeService, PersistAndRestoreThemeService>();
            containerRegistry.Register<IPersistAndRestoreSinclaireCoefficientService, PersistAndRestoreSinclaireCoefficientService>();
            containerRegistry.RegisterSingleton<IThemeSelectorService, ThemeSelectorService>();
            containerRegistry.RegisterSingleton<IFlyoutService, FlyoutService>();
            containerRegistry.RegisterSingleton<ISinclaireCoefficientService, SinclaireCoefficientService>();
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
