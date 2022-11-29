using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace WeightliftingManagment.Moduls.Judge
{
    public class JudgeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public JudgeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
