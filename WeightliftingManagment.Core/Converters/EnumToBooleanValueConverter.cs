using System.Globalization;

using WeightliftingManagment.Core.BaseType;

namespace WeightliftingManagment.Core.Converters
{
    public class EnumToBooleanValueConverter : BaseMarkupValueConverter
    {
        public Type EnumType { get; set; }
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string enumString && Enum.IsDefined(EnumType, value))
            {
                var enumValue = Enum.Parse(EnumType, enumString);
                return enumValue.Equals(value);
            }

            return false;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => parameter is string enumString ? Enum.Parse(EnumType, enumString) : null;
    }
}
