
using Prism;
using Prism.Regions;

namespace WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase
{
    public class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest, IActiveAware
    {
        protected IRegionManager RegionManager { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public RegionViewModelBase(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) => continuationCallback(true);

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        #region IActiveAwere

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }

        public event EventHandler IsActiveChanged;
        protected void RaiseIsActiveChanged() => IsActiveChanged?.Invoke(this, EventArgs.Empty);

        #endregion
    }
}
