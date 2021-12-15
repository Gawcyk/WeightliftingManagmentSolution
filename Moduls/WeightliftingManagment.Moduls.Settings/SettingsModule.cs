using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Ioc;
using Prism.Modularity;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Moduls.Settings.Views;

namespace WeightliftingManagment.Moduls.Settings
{
    public class SettingsModule : IModule
    {
        private readonly IFlyoutService _flyoutService;

        public SettingsModule(IFlyoutService flyoutService)
        {
            _flyoutService = flyoutService;
            _flyoutService.SetDefaultFlyoutRegion(Regions.FlyoutRegion);
        }
        public void OnInitialized(IContainerProvider containerProvider) => _flyoutService.RegisterFlyoutWithDefaultRegion<ThemeSettings>(FlyoutKeys.ThemeFlyout);

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
