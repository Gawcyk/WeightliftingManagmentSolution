using Moq.AutoMock;

using Prism.Regions;

using WeightliftingManagment.Core.Constans;
using WeightliftingManagment.Core.Contracts;

using Xunit;

namespace WeightliftingManagment.ViewModels.Tests
{
    public class ShellWindowViewModelTests
    {
        [Fact()]
        public void NavigationCommandExecuted_WhenParamIsThemeFlyout_ThenOpenThemeFlyout()
        {
            AutoMocker mocker = new();
            var flyoutService = mocker.GetMock<IFlyoutService>();
            var sut = mocker.CreateInstance<ShellWindowViewModel>();
            var key = FlyoutKeys.ThemeFlyout;

            sut.NavigationCommand.Execute(key);

            flyoutService.Verify(x => x.OpenFlyout(key));
        }

        [Theory]
        [InlineData(PageKeys.Main)]
        [InlineData(PageKeys.Settings)]
        [InlineData(PageKeys.AppSettings)]
        [InlineData(PageKeys.BodyWeight)]
        [InlineData(PageKeys.CompetitionSettings)]
        public void NavigationCommandExecuted_WhenParamIsPageKey_ThenRequestNavigate(string key)
        {
            AutoMocker mocker = new();
            var regionManager = mocker.GetMock<IRegionManager>();
            var sut = mocker.CreateInstance<ShellWindowViewModel>();

            sut.NavigationCommand.Execute(key);

            regionManager.Verify(x => x.RequestNavigate(Regions.MainRegion, key));
        }
    }
}