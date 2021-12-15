namespace WeightliftingManagment.Core.FlyoutManager
{
    public class FlyoutEventArgs : EventArgs
    {
        public FlyoutAction FlyoutAction { get; set; }

        public FlyoutEventArgs(FlyoutAction flyoutAction)
        {
            FlyoutAction = flyoutAction;
        }
    }
}