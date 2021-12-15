
using System.Windows.Input;

using WeightliftingManagment.Core.FlyoutManager;

namespace WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase
{
    public abstract class FlyoutViewModelBase : ViewModelBase, IFlyout
    {
        public event EventHandler<FlyoutEventArgs> OnClosed;
        public event EventHandler<FlyoutEventArgs> OnOpened;
        public event EventHandler<FlyoutEventArgs> OnOpenChanged;
        private bool _isOpen;
        private string _position = FlyoutPosition.Right;
        private string _header;
        private string _theme = FlyoutTheme.Adapt;
        private bool _isModal = false;
        private bool _areAnimationsEnabled = true;
        private bool _animateOpacity;
        private ICommand _closeCommand;
        private MouseButton _externalCloseButton;
        private bool _closeButtonIsCancel;
        private bool _isPinned;

        /// <summary>
        /// Bindable property to determine open/closed staus of flyout, based on private field isOpen.
        /// Generally set by FlyoutManager calling Open or Close method.
        /// Initial value can be set by concrete child class.
        /// </summary>
        public bool IsOpen
        {
            get => _isOpen;
            set {
                OnChanging(value);
                SetProperty(ref _isOpen, value);
            }
        }

        /// <summary>
        /// Bindable property to determine position of flyout in window.
        /// Possible values are stored in FlyoutPosition class and can be used to set property in the concrete class.
        /// </summary>
        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        /// <summary>
        /// Bindable property to determine header of flyout.
        /// Value set by concrete child class.
        /// </summary>
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        /// <summary>
        /// Bindable property to determine theme of flyout.
        /// Possible values are stored in FlyoutTheme class and can be used to set property in concrete class.
        /// </summary>
        public string Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        /// <summary>
        /// Bindable property to determine whether the Flyout is modal or not
        /// </summary>
        public bool IsModal
        {
            get => _isModal;
            set => SetProperty(ref _isModal, value);
        }

        /// <summary>
        /// Bindable property to determine whether flyout animations are enabled
        /// </summary>
        public bool AreAnimationsEnabled
        {
            get => _areAnimationsEnabled;
            set => SetProperty(ref _areAnimationsEnabled, value);
        }

        /// <summary>
        /// Is opacity animated.
        /// </summary>
        public bool AnimateOpacity
        {
            get => _animateOpacity;
            set => SetProperty(ref _animateOpacity, value);
        }

        /// <summary>
        /// Command to execute when the close button is pressed
        /// </summary>
        public ICommand CloseCommand
        {
            get => _closeCommand;
            set => SetProperty(ref _closeCommand, value);
        }

        /// <summary>
        /// Designate a mousebutton that will close the flyout when pressed outside of its bounds.
        /// </summary>
        public MouseButton ExternalCloseButton
        {
            get => _externalCloseButton;
            set => SetProperty(ref _externalCloseButton, value);
        }

        /// <summary>
        /// Does the close button act as a cancel button.
        /// </summary>
        public bool CloseButtonIsCancel
        {
            get => _closeButtonIsCancel;
            set => SetProperty(ref _closeButtonIsCancel, value);
        }

        /// <summary>
        /// Is the flyout pinned.
        /// </summary>
        public bool IsPinned
        {
            get => _isPinned;
            set => SetProperty(ref _isPinned, value);
        }

        /// <summary>
        /// Whether the flyout is currently able to open.
        /// Default: returns true
        /// Override in concrete child class to implement custom logic.
        /// </summary>
        /// <param name="flyoutParameters">Instance of <typeparamref name="FlyoutParameters"/> containing information on current Open request.</param>
        /// <returns>True if flyout can open, false if not.</returns>
        public virtual bool CanOpen(FlyoutParameters flyoutParameters) => true;

        /// <summary>
        /// Whether the flyout is currently able to close.
        /// Default: returns true.
        /// Override in concrete child class to implement custom logic.
        /// </summary>
        /// <param name="flyoutParameters">Instance of <typeparamref name="FlyoutParameters"/> containing information on current Close request.</param>
        /// <returns>True if flyout can close, false if not.</returns>
        public virtual bool CanClose(FlyoutParameters flyoutParameters) => true;

        /// <summary>
        /// Event delegate called on flyout closing with FlyoutEventArgs.
        /// </summary>
        /// <param name="flyoutParameters">Instance of <typeparamref name="FlyoutParameters"/> containing information on current Close request.</param>
        protected virtual void OnClosing(FlyoutParameters flyoutParameters) { }

        /// <summary>
        /// Event delegate called on flyout closing, with FlyoutEventArgs.
        /// </summary>
        /// <param name="flyoutParameters">Instance of <typeparamref name="FlyoutParameters"/> containing information on current Close request.</param>
        protected virtual void OnOpening(FlyoutParameters flyoutParameters) { }

        /// <summary>
        /// Event delegate called on flyout open status changed, with FlyoutEventArgs containing open/close direction.
        /// </summary>
        /// <param name="isOpening"></param>
        protected virtual void OnChanging(bool isOpening)
        {
            var flyoutAction = isOpening ? FlyoutAction.Opening : FlyoutAction.Closing;
            var flyoutEventArgs = new FlyoutEventArgs(flyoutAction);
            OnOpenChanged?.Invoke(this, flyoutEventArgs);

            if (flyoutAction == FlyoutAction.Opening)
            {
                OnOpened?.Invoke(this, flyoutEventArgs);
            }

            if (flyoutAction == FlyoutAction.Closing)
            {
                OnClosed?.Invoke(this, flyoutEventArgs);
            }
        }

        /// <summary>
        /// Close the flyout.
        /// </summary>
        /// <param name="flyoutParameters"></param>
        public void Close(FlyoutParameters flyoutParameters)
        {
            OnClosing(flyoutParameters);
            IsOpen = false;
        }

        public void Close() => Close(new FlyoutParameters());

        /// <summary>
        /// Open the flyout.
        /// </summary>
        /// <param name="flyoutParameters"></param>
        public void Open(FlyoutParameters flyoutParameters)
        {
            OnOpening(flyoutParameters);
            IsOpen = true;
        }

        public void Open() => Open(new FlyoutParameters());
    }
}
