
using Prism.Ioc;
using Prism.Modularity;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels;
using WeightliftingManagment.Moduls.ParticipantBodyWeight.Views;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight
{
    public class ParticipantBodyWeightModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BodyWeight, BodyWeightViewModel>(PageKeys.BodyWeight);
            containerRegistry.RegisterDialog<AddParticipant>(DialogName.AddParticipant);
        }
    }
}
