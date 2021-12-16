using System.Windows.Threading;

using Prism.Commands;

using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Dialogs;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.Dialogs.ViewModels
{
    public class NotificationDialogViewModel : DialogViewModelBase
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

        private DialogType _dialogType;
        public DialogType DialogType
        {
            get => _dialogType;
            set => SetProperty(ref _dialogType, value);
        }

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand => _closeDialogCommand ??= new DelegateCommand(ExecuteCloseDialogCommand);

        private void ExecuteCloseDialogCommand() => RaiseRequestClose(new DialogResult(ButtonResult.OK));

        public NotificationDialogViewModel()
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
                CloseDialogCommand.Execute();
            }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
            Message = parameters.GetValue<string>("Message");
            CloseTime = parameters.GetValue<int>("CloseTime");
            TimeWindowClose = parameters.GetValue<int>("CloseTime");
            var dialogType = parameters.GetValue<string>("DialogType");
            DialogType = dialogType switch {
                "Alert" => DialogType.Alert,
                "Information" => DialogType.Information,
                _ => throw new ArgumentNullException(paramName: dialogType, "Nie podano typu")
            };
        }
    }
}
