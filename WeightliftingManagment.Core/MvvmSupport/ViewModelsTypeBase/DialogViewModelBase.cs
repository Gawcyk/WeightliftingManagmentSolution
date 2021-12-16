using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Services.Dialogs;
using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase
{
    public class DialogViewModelBase : ErrorsBindableObject, IDialogAware
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public event Action<IDialogResult> RequestClose;
        public virtual void RaiseRequestClose(IDialogResult dialogResult) => RequestClose?.Invoke(dialogResult);
        public virtual void RaiseRequestClose(ButtonResult result) => RequestClose?.Invoke(new DialogResult(result));
        public virtual void RaiseRequestClose(ButtonResult result, IDialogParameters pairs) => RequestClose?.Invoke(new DialogResult(result, pairs));

        public virtual bool CanCloseDialog() => true;

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
