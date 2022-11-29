using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WeightliftingManagment.CustomControl
{
    public class HamburgerGlyphMenuItem : ListBoxItem
    {
        static HamburgerGlyphMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerGlyphMenuItem), new FrameworkPropertyMetadata(typeof(HamburgerGlyphMenuItem)));
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HamburgerGlyphMenuItem), new PropertyMetadata(string.Empty));


        public string? Glyph
        {
            get => (string?)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }

        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(HamburgerGlyphMenuItem), new PropertyMetadata(null));

        public Brush SelectionIndicatorColor
        {
            get => (Brush)GetValue(SelectionIndicatorColorProperty);
            set => SetValue(SelectionIndicatorColorProperty, value);
        }

        public static readonly DependencyProperty SelectionIndicatorColorProperty =
            DependencyProperty.Register("SelectionIndicatorColor", typeof(Brush), typeof(HamburgerGlyphMenuItem), new PropertyMetadata(Brushes.Blue));

        public ICommand SelectionCommand
        {
            get => (ICommand)GetValue(SelectionCommandProperty);
            set => SetValue(SelectionCommandProperty, value);
        }

        public static readonly DependencyProperty SelectionCommandProperty =
            DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(HamburgerGlyphMenuItem), new PropertyMetadata(null));
    }
}