
using System.Collections.ObjectModel;

using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Dialogs;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Domain.Model;
using WeightliftingManagment.Moduls.ParticipantBodyWeight.DesignTest;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public class BodyWeightViewModel : RegionViewModelBase
    {
        private readonly IDialogService _dialogService;

        public BodyWeightViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            _dialogService = dialogService;
            Participants = ParticipantCollectionData.GetParticipantCollection();

            //var part = new Participant();
            //part.CleanJerks[0].Value = 100;
            //var part2 = new Participant();
            //part2.CleanJerks[0].Value = 100;
            //var part3 = new Participant();
            //part3.CleanJerks[0].Value = 100;
            //var part4 = new Participant();
            //part4.CleanJerks[0].Value = 100;


            //Participants = new ParticipantCollection() {
            //    part, part2, part3, part4
            //};
        }

        private ParticipantCollection _participants = new();
        public ParticipantCollection Participants
        {
            get => _participants;
            set => SetProperty(ref _participants, value);
        }

        //private DelegateCommand _alertCommand;
        //public DelegateCommand AlertCommand => _alertCommand ??= new DelegateCommand(ExecuteAlert);

        //private void ExecuteAlert() => _dialogService.ShowAlert("Title", "message");

        //private DelegateCommand _notificationCommand;
        //public DelegateCommand NotificationCommand => _notificationCommand ??= new DelegateCommand(ExecuteNotofocation);

        //private void ExecuteNotofocation() => _dialogService.ShowNotification("Title", "message");

        //private DelegateCommand _questionCommand;
        //public DelegateCommand QuestionCommand => _questionCommand ??= new DelegateCommand(ExecuteQuestion);

        //private void ExecuteQuestion() => _dialogService.ShowQuestion("Title", "messege");

        private DelegateCommand _addParticipantCommand;
        public DelegateCommand AddParticipantCommand => _addParticipantCommand ??= new DelegateCommand(ExecuteAddParticipant);
        private void ExecuteAddParticipant()
        {
            var participant = _dialogService.ShowAddParticipant();
            if (participant is not null)    
                Participants.AddNewParticipant(participant);
        }

        private DelegateCommand<Participant> _editParticipantCommand;
        public DelegateCommand<Participant> EditParticipantCommand => _editParticipantCommand ??= new DelegateCommand<Participant>(ExecuteEditParticipant);
        private void ExecuteEditParticipant(Participant participant)
        {
            var particip = _dialogService.ShowEditParticipant(participant);
            var oldParticipant = Participants.GetParticipantByID(participant.ParticipantId);
            oldParticipant = particip;
        }
    }
}
