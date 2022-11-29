using System.Windows;
using System.Windows.Input;

using Prism.Commands;
using Prism.Regions;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.ViewModels
{
    public class ShellWindowViewModel : ViewModelBase
    {
        #region Filds

        private readonly IRegionManager _regionManager;
        private readonly IFlyoutService _flyoutService;
        private DelegateCommand<string> _navigationCommand;
        private ICommand _closeCommand;

        #endregion

        public ShellWindowViewModel(IRegionManager regionManager, IFlyoutService flyoutService)
        {
            _regionManager = regionManager;
            _flyoutService = flyoutService;
        }

        #region Property

        #endregion

        #region Commands

        public ICommand CloseCommand => _closeCommand ??= new DelegateCommand(ExecuteClose);
        public DelegateCommand<string> NavigationCommand => _navigationCommand ??= new DelegateCommand<string>(ExecuteNavigationCommand);

        #endregion

        #region Methods

        private void ExecuteClose() => Application.Current.MainWindow.Close();

        private void ExecuteNavigationCommand(string pageKey)
        {
            switch (pageKey)
            {
                case "Close": ExecuteClose();
                    break;
                case FlyoutKeys.ThemeFlyout: _flyoutService.OpenFlyout(FlyoutKeys.ThemeFlyout);
                    break;
                default:
                    _regionManager.RequestNavigate(Regions.MainRegion, pageKey);
                    break;
            }
        }

        #endregion

    }
}
