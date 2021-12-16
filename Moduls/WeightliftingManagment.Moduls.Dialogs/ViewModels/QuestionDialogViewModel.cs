using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.Dialogs.ViewModels
{
    public class QuestionDialogViewModel : DialogViewModelBase
    {
        private readonly DispatcherTimer _timer;

        private int _closeTime;
        public int CloseTime
        {
            get => _closeTime;
            set => SetProperty(ref _closeTime, value);
        }
        private int _timeWindowClose;
        public int TimeWindowClose
        {
            get => _timeWindowClose;
            set => SetProperty(ref _timeWindowClose, value);
        }
        public QuestionDialogViewModel()
        {
            _timer = new DispatcherTimer {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true
            };
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            CloseTime--;

            if (CloseTime == 0)
            {
                CancelCommand.Execute();
            }
        }

        private DelegateCommand _okCommand;
        public DelegateCommand OkCommand => _okCommand ??= new DelegateCommand(ExecuteOkCommand);

        private void ExecuteOkCommand() => RaiseRequestClose(ButtonResult.Yes);

        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(ExecuteCancelCommand);

        private void ExecuteCancelCommand() => RaiseRequestClose(ButtonResult.No);

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            Message = parameters.GetValue<string>("Message");
            CloseTime = parameters.GetValue<int>("CloseTime");
            TimeWindowClose = parameters.GetValue<int>("CloseTime");
        }
    }
}
