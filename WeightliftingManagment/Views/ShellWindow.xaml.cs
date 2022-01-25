
using MahApps.Metro.Controls;

using Prism.Regions;

using WeightliftingManagment.Core.Constans;

namespace WeightliftingManagment.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            RegionManager.SetRegionName(hamburgerMenuContentControl, Regions.MainRegion);
            RegionManager.SetRegionManager(hamburgerMenuContentControl, regionManager);
        }
    }
}
