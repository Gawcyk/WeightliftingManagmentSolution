﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Prism.Ioc;
using Prism.Regions;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.FlyoutManager;

namespace WeightliftingManagment.Services
{
    public  class FlyoutService : IFlyoutService
    {
        private readonly IDictionary<string, IFlyout> _flyouts;
        private readonly IContainerProvider _container;
        private string _defaultFlyoutRegion;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container">Unity container, generally passed by dependency injection.</param>
        /// <param name="regionManager">Region manager, generally passed by dependency injection.</param>
        public FlyoutService(IContainerProvider container, IRegionManager regionManager)
        {
            _container = container;
            RegionManager = regionManager;
            _flyouts = new Dictionary<string, IFlyout>();
        }

        public bool ContainsFlyout(string key) => _flyouts.ContainsKey(key);

        public void SetDefaultFlyoutRegion(string regionName) => _defaultFlyoutRegion = regionName;

        /// <summary>
        /// The region manager controlling the region where the Flyouts are to be displayed.
        /// Made public to allow use of sub-region managers.
        /// </summary>
        public IRegionManager RegionManager { get; set; }

        public void RegisterFlyoutWithDefaultRegion<TView>(string flyoutKey) where TView : FrameworkElement
        {
            if (string.IsNullOrEmpty(_defaultFlyoutRegion))
            {
                throw new NullReferenceException("Default region not set.");
            }

            RegisterFlyout<TView>(flyoutKey, _defaultFlyoutRegion);
        }

        public void RegisterFlyoutWithDefaultRegion<TView>(string flyoutKey, IFlyout viewModel) where TView : FrameworkElement
        {
            if (string.IsNullOrEmpty(_defaultFlyoutRegion))
            {
                throw new NullReferenceException("Default region not set.");
            }

            RegisterFlyout<TView>(flyoutKey, _defaultFlyoutRegion, viewModel);
        }

        /// <summary>
        /// Register Flyout with FlyoutManager.  The viewmodel will be obtained from the view, so a view-first approach must
        /// be used.  This method is compatible with autowireviewmodel.  The viewmodel produced must derive from IFlyout.
        /// </summary>
        /// <typeparam name="TView">Type of the view used for the flyout.</typeparam>
        /// <param name="flyoutKey">A key to identify the flyout.</param>
        /// <param name="flyoutRegion">The region name in which to display the flyout.</param>
        public void RegisterFlyout<TView>(string flyoutKey, string flyoutRegion) where TView : FrameworkElement
        {
            var flyoutView = ResolveFlyoutView<TView>();

            var flyoutViewModel = GetFlyoutViewModelFromView(flyoutView);

            AddFlyoutToCollection(flyoutKey, flyoutRegion, flyoutViewModel, flyoutView);
        }

        /// <summary>
        /// Register Flyout with FlyoutManager, a viewmodel must be supplied that derives from IFlyout.
        /// </summary>
        /// <typeparam name="TView">Type of the view used for the flyout.</typeparam>
        /// /// <param name="flyoutKey">A key to identify the flyout.</param>
        /// <param name="flyoutRegion">The region name in which to display the flyout.</param>
        /// <param name="flyoutViewModel"></param>
        public void RegisterFlyout<TView>(string flyoutKey, string flyoutRegion, IFlyout flyoutViewModel) where TView : FrameworkElement
        {
            var flyoutView = ResolveFlyoutView<TView>();

            AddFlyoutToCollection(flyoutKey, flyoutRegion, flyoutViewModel, flyoutView);
        }

        /// <summary>
        /// Remove the specified Flyout from this FlyoutManager.
        /// </summary>
        /// <param name="key">The key that identifies the flyout.</param>
        public void UnRegisterFlyout(string key) => _flyouts.Remove(key);

        /// <summary>
        /// Remove the specified Flyout from this FlyoutManager
        /// </summary>
        /// <param name="flyout">The flyout to remove.</param>
        public void UnRegisterFlyout(IFlyout flyout)
        {
            var items = _flyouts.Where(kvp => kvp.Value == flyout).ToList();
            items.ForEach(item => UnRegisterFlyout(item.Key));
        }
        /// <summary>
        /// Attempt to open the identified flyout, passing default flyoutparameters to the flyout's CanOpen method.
        /// </summary>
        /// <param name="key">Identifies the flyout to open.</param>
        /// <returns>True if opened, false if CanOpen prevented opening.</returns>
        public bool OpenFlyout(string key) => OpenFlyout(key, new FlyoutParameters(), false);

        /// <summary>
        /// Attempt to open the identified flyout, passing the specified flyoutparameters to the flyout's CanOpen method.
        /// </summary>
        /// <param name="key">Identifies the flyout to open.</param>
        /// <param name="flyoutParameters">Passed to the flyouts CanOpen method, and indirectly to OnOpening.</param>
        /// <returns>True if opened, false if CanOpen prevented opening.</returns>
        public bool OpenFlyout(string key, FlyoutParameters flyoutParameters) => OpenFlyout(key, flyoutParameters, false);

        /// <summary>
        /// Attempts to open the identified flyout
        /// Default flyoutparameters are passed to the flyout's CanOpen method and the result returned,
        /// but if forceOpen is true, the flyout is opened even if CanOpen returns false.
        /// </summary>
        /// <param name="key">Identifies the flyout to open.</param>
        /// <param name="forceOpen">Whether to force open the flyout.</param>
        /// <returns>The result of the identified flyout's CanOpen method.</returns>
        public bool OpenFlyout(string key, bool forceOpen) => OpenFlyout(key, new FlyoutParameters(), forceOpen);

        /// <summary>
        /// Attempts to open the identified flyout
        /// The specified flyoutparameters are passed to the flyout's CanOpen method and the result returned,
        /// but if forceOpen is true, the flyout is opened even if CanOpen returns false.
        /// </summary>
        /// <param name="key">Identifies the flyout to open.</param>
        /// <param name="flyoutParameters">Passed to the flyouts CanOpen method, and indirectly to OnOpening.</param>
        /// <param name="forceOpen">Whether to force open the flyout.</param>
        /// <returns>The result of the identified flyout's CanOpen method.</returns>
        public bool OpenFlyout(string key, FlyoutParameters flyoutParameters, bool forceOpen)
        {
            var flyoutToActivate = _flyouts[key];
            var canOpen = flyoutToActivate.CanOpen(flyoutParameters);

            if (!forceOpen && !canOpen)
            {
                return false;
            }
            if (flyoutToActivate.IsOpen)
            {
                return false;
            }

            flyoutToActivate.Open(flyoutParameters);

            return canOpen;
        }

        /// <summary>
        /// Attempt to close the identified flyout, passing default flyoutparameters to the flyout's CanClose method.
        /// </summary>
        /// <param name="key">Identifies the flyout to close.</param>
        /// <returns>True if closed, false if CanClose prevented closing.</returns>
        public bool CloseFlyout(string key) => CloseFlyout(key, new FlyoutParameters(), false);

        /// <summary>
        /// Attempt to close the identified flyout, passing the provided flyoutparameters to the flyout's CanClose method.
        /// </summary>
        /// <param name="key">Identifies the flyout to close.</param>
        /// <param name="flyoutParameters">Passed to the flyouts CanClose method, and indirectly to OnClosing.</param>
        /// <returns>True if closed, false if CanClose prevented closing.</returns>
        public bool CloseFlyout(string key, FlyoutParameters flyoutParameters) => CloseFlyout(key, flyoutParameters, false);

        /// <summary>
        /// Closes the identified flyout, passing default flyoutparameters to the flyout's CanClose method.
        /// If forceClose is true, the flyout will be closed even if CanClose returns false.  The result of CanClose
        /// will be returned by this method even if closure is forced.
        /// </summary>
        /// <param name="key">Identifies the flyout to close.</param>
        /// <param name="forceClose">Force flyout closure even if CanClose returns false.</param>
        /// <returns>The results of the indentified flyouts CanClose method.</returns>
        public bool CloseFlyout(string key, bool forceClose) => CloseFlyout(key, new FlyoutParameters(), forceClose);

        /// <summary>
        /// Closes the identified flyout, passing the provided flyoutparameters to the flyout's CanClose method.
        /// If forceClose is true, the flyout will be closed even if CanClose returns false.  The result of CanClose
        /// will be returned by this method even if closure is forced.
        /// </summary>
        /// <param name="key">Identifies the flyout to close.</param>
        /// <param name="flyoutParameters">Passed to the flyouts CanClose method, and indirectly to OnClosing.</param>
        /// <param name="forceClose">Force flyout closure even if CanClose returns false.</param>
        /// <returns>The results of the indentified flyouts CanClose method.</returns>
        public bool CloseFlyout(string key, FlyoutParameters flyoutParameters, bool forceClose)
        {
            var flyoutToClose = _flyouts[key];
            var canClose = flyoutToClose.CanClose(flyoutParameters);

            if (!forceClose && !canClose)
            {
                return false;
            }

            flyoutToClose.Close(flyoutParameters);
            return canClose;
        }

        private static IFlyout GetFlyoutViewModelFromView(FrameworkElement flyoutView)
        {
            if (flyoutView.DataContext is not IFlyout flyoutViewModel)
            {
                throw new ArgumentException(@"Type passed must have an auto-wired view model that implements IFlyout.
                                               If auto-wiring is not used then pass the viewmodel instance to the overloaded
                                               RegisterFlyout method.");
            }

            return flyoutViewModel;
        }

        private FrameworkElement ResolveFlyoutView<T>() => _container.Resolve<T>() as FrameworkElement;

        private void RegisterFlyoutWithRegion(FrameworkElement flyoutView, string flyoutRegion) => RegionManager.RegisterViewWithRegion(flyoutRegion, () => flyoutView);

        private void AddFlyoutToCollection(string flyoutKey, string flyoutRegion, IFlyout flyoutViewModel, FrameworkElement flyoutView)
        {
            RegisterFlyoutWithRegion(flyoutView, flyoutRegion);

            _flyouts.Add(flyoutKey, flyoutViewModel);
        }

        public bool IsOpen(string key)
        {
            if (ContainsFlyout(key))
            {
                return _flyouts[key].IsOpen;
            }
            return false;
        }
    }
}
