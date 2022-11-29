
using System.Globalization;
using System.Windows;

using WeightliftingManagment.Core.BaseType;

namespace WeightliftingManagment.Core.Converters
{
    public class BoolToVisibilityValueConverter : BaseMarkupValueConverter
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => parameter == null
                 ? (bool)value ? Visibility.Visible : Visibility.Collapsed
                 : (bool)value ? Visibility.Collapsed : Visibility.Visible;

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
