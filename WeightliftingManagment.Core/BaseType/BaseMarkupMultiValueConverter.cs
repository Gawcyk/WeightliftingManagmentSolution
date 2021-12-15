using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WeightliftingManagment.Core.BaseType
{
    [MarkupExtensionReturnType(typeof(IMultiValueConverter))]
    public abstract class BaseMarkupMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        protected abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        protected abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Convert(values, targetType, parameter, culture);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            try
            {
                return ConvertBack(value, targetTypes, parameter, culture);
            }
            catch
            {
                var r = new object[targetTypes.Length];
                for (var i = 0; i < targetTypes.Length; i++)
                {
                    r[i] = DependencyProperty.UnsetValue;
                }
                return r;
            }
        }
    }
}
