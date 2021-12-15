using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
