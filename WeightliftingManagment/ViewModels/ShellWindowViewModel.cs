using System.Windows;
using System.Windows.Input;

using Prism.Commands;
using Prism.Regions;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Helpers;

namespace WeightliftingManagment.ViewModels
{
    public class ShellWindowViewModel : ViewModelBase
    {
        #region Filds

        private readonly IRegionManager _regionManager;
        private readonly IFlyoutService _flyoutService;
        private ICommand _menuFileExitCommand;
        private DelegateCommand<string> _navigationCommand;
        private DelegateCommand<string> _openFlyoutCommand;

        #endregion

        #region Commands

        public DelegateCommand<string> OpenFlyoutCommand => _openFlyoutCommand ??= new DelegateCommand<string>(ExecuteOpenFlyoutCommand);

        public ICommand MenuFileExitCommand => _menuFileExitCommand ??= new DelegateCommand(OnMenuFileExit);

        public DelegateCommand<string> NavigationCommand => _navigationCommand ??= new DelegateCommand<string>(OnNavigationCommand);

        #endregion


        public ShellWindowViewModel(IRegionManager regionManager, IFlyoutService flyoutService)
        {
            _regionManager = regionManager;
            _flyoutService = flyoutService;
        }



        #region Methods

        private void ExecuteOpenFlyoutCommand(string key) => _flyoutService.OpenFlyout(key);

        private void OnMenuFileExit() => Application.Current.Shutdown();

        private void OnNavigationCommand(string pageKey) => _regionManager.RequestNavigate(Regions.MainRegion,pageKey);

        #endregion

    }
}
