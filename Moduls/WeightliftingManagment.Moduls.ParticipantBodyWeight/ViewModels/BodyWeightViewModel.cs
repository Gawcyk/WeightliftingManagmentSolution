using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Regions;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public class BodyWeightViewModel : RegionViewModelBase
    {
        public BodyWeightViewModel(IRegionManager regionManager) : base(regionManager)
        {

        }
    }
}
