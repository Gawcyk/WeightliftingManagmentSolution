using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Prism.Regions;

using WeightliftingManagment.Core.FlyoutManager;

namespace WeightliftingManagment.Core.Contracts
{
    public interface IFlyoutService
    {
        bool IsOpen(string key);

        IRegionManager RegionManager { get; set; }

        bool ContainsFlyout(string key);

        void SetDefaultFlyoutRegion(string regionName);

        void RegisterFlyoutWithDefaultRegion<TView>(string flyoutKey) where TView : FrameworkElement;

        void RegisterFlyoutWithDefaultRegion<TView>(string flyoutKey, IFlyout viewModel) where TView : FrameworkElement;

        void RegisterFlyout<T>(string flyoutKey, string regionName) where T : FrameworkElement;

        void RegisterFlyout<T>(string flyoutKey, string regionName, IFlyout flyoutViewModel) where T : FrameworkElement;

        void UnRegisterFlyout(string key);

        void UnRegisterFlyout(IFlyout flyout);

        bool OpenFlyout(string key);

        bool OpenFlyout(string key, FlyoutParameters flyoutParameters);

        bool OpenFlyout(string key, bool forceOpen);

        bool OpenFlyout(string key, FlyoutParameters flyoutParameters, bool forceOpen);

        bool CloseFlyout(string key);

        bool CloseFlyout(string key, FlyoutParameters flyoutParameters);

        bool CloseFlyout(string key, bool forceClose);

        bool CloseFlyout(string key, FlyoutParameters flyoutParameters, bool forceClose);
    }
}
