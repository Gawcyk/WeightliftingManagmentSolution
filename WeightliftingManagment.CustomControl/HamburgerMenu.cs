using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeightliftingManagment.CustomControl
{
    public class HamburgerMenu : ContentControl
    {
        public new List<HamburgerMenuItem> Content
        {
            get => (List<HamburgerMenuItem>)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly new DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(List<HamburgerMenuItem>), typeof(HamburgerMenu),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        static HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));
        }

        public override void BeginInit()
        {
            Content = new List<HamburgerMenuItem>();
            base.BeginInit();
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(true));


        public Brush MenuIconColor
        {
            get => (Brush)GetValue(MenuIconColorProperty);
            set => SetValue(MenuIconColorProperty, value);
        }

        public static readonly DependencyProperty MenuIconColorProperty =
            DependencyProperty.Register("MenuIconColor", typeof(System.Windows.Media.Brush), typeof(HamburgerMenu), new PropertyMetadata(System.Windows.Media.Brushes.White));


        public Brush SelectionIndicatorColor
        {
            get => (Brush)GetValue(SelectionIndicatorColorProperty);
            set => SetValue(SelectionIndicatorColorProperty, value);
        }

        public static readonly DependencyProperty SelectionIndicatorColorProperty =
            DependencyProperty.Register("SelectionIndicatorColor", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.Red));

        public Brush MenuItemForeground
        {
            get => (Brush)GetValue(MenuItemForegroundProperty);
            set => SetValue(MenuItemForegroundProperty, value);
        }

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register("MenuItemForeground", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(Brushes.Transparent));

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(HamburgerMenu), new PropertyMetadata(0));
    }
}
