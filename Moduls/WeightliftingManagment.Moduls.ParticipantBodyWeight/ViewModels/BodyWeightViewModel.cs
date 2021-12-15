using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Dialogs;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public class BodyWeightViewModel : RegionViewModelBase
    {
        private readonly IDialogService _dialogService;

        public BodyWeightViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            _dialogService = dialogService;
        }

        private DelegateCommand _alertCommand;
        public DelegateCommand AlertCommand => _alertCommand ??= new DelegateCommand(ExecuteAlert);

        private void ExecuteAlert() => _dialogService.ShowAlert("Title", "message");

        private DelegateCommand _notificationCommand;
        public DelegateCommand NotificationCommand => _notificationCommand ??= new DelegateCommand(ExecuteNotofocation);

        private void ExecuteNotofocation() => _dialogService.ShowNotification("Title", "message");

        private DelegateCommand _questionCommand;
        public DelegateCommand QuestionCommand => _questionCommand ??= new DelegateCommand(ExecuteQuestion);

        private void ExecuteQuestion() => _dialogService.ShowQuestion("Title", "messege");
    }
}
