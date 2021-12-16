using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WeightliftingManagment.Core.AttachedProperties
{
    public class GridExtensions
    {
        public static string GetPosition(DependencyObject obj) => (string)obj.GetValue(PositionProperty);
        public static void SetPosition(DependencyObject obj, string value) => obj.SetValue(PositionProperty, value);
        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position", typeof(string), typeof(GridExtensions), new PropertyMetadata("", PositionChangedCallback));

        private static void PositionChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is UIElement element)
            {
                var position = args.NewValue.ToString();
                if (position.Contains(',') && position.Split(",").Length == 2)
                {
                    var row = int.Parse(position.Split(",")[0]);
                    var column = int.Parse(position.Split(",")[1]);
                    Grid.SetRow(element, row);
                    Grid.SetColumn(element, column);
                }
            }
        }

        public static string GetColumnWidth(DependencyObject obj) => (string)obj.GetValue(ColumnWidthProperty);
        public static void SetColumnWidth(DependencyObject obj, string value) => obj.SetValue(ColumnWidthProperty, value);
        // Using a DependencyProperty as the backing store for ColumnWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.RegisterAttached("ColumnWidth", typeof(string), typeof(GridExtensions), new PropertyMetadata("", ColumnWidthChangedCallback));

        private static void ColumnWidthChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is not Grid grid)
            {
                return;
            }

            grid.ColumnDefinitions.Clear();

            var definitions = args.NewValue.ToString();
            if (string.IsNullOrEmpty(definitions))
            {
                return;
            }

            var heights = definitions.Split(',');

            foreach (var height in heights)
            {
                if (height == "Auto")
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                }
                else if (height.EndsWith("*"))
                {
                    var height2 = height.Replace("*", "");
                    if (string.IsNullOrEmpty(height2))
                    {
                        height2 = "1";
                    }

                    var numHeight = int.Parse(height2);
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(numHeight, GridUnitType.Star) });
                }
                else
                {
                    var numHeight = int.Parse(height);
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(numHeight, GridUnitType.Pixel) });
                }
            }
        }

        public static string GetRowHeights(DependencyObject obj) => (string)obj.GetValue(RowHeightsProperty);
        public static void SetRowHeights(DependencyObject obj, string value) => obj.SetValue(RowHeightsProperty, value);
        // Using a DependencyProperty as the backing store for RowHeights.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowHeightsProperty = DependencyProperty.RegisterAttached("RowHeights", typeof(string), typeof(GridExtensions), new PropertyMetadata("", RowHeightsChangedCallback));
        private static void RowHeightsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is not Grid grid)
            {
                return;
            }

            grid.RowDefinitions.Clear();

            var definitions = args.NewValue.ToString();
            if (string.IsNullOrEmpty(definitions))
            {
                return;
            }

            var heights = definitions.Split(',');

            foreach (var height in heights)
            {
                if (height == "Auto")
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
                else if (height.EndsWith("*"))
                {
                    var height2 = height.Replace("*", "");
                    if (string.IsNullOrEmpty(height2))
                    {
                        height2 = "1";
                    }

                    var numHeight = int.Parse(height2);
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(numHeight, GridUnitType.Star) });
                }
                else
                {
                    var numHeight = int.Parse(height);
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(numHeight, GridUnitType.Pixel) });
                }
            }
        }
    }
}
