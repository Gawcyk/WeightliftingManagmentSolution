using Prism.Ioc;
using Prism.Modularity;

using WeightliftingManagment.Core.Constans;

namespace WeightliftingManagment.Moduls.CompetitionSettings
{
    public class CompetitionSettingsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }
        public void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry.RegisterForNavigation<Views.CempetitionSettings, ViewModels.CompetitionSettingsViewModel>(PageKeys.CompetitionSettings);
    }
}
