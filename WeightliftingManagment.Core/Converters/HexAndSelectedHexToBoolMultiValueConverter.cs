using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.Core.BaseType;

namespace WeightliftingManagment.Core.Converters
{
    public class HexAndSelectedHexToBoolMultiValueConverter : BaseMarkupMultiValueConverter
    {
        private string _hex;
        private string _hexSelected;
        protected override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            _hex = values[0].ToString();
            _hexSelected = values[1].ToString();
            return _hex == _hexSelected;
        }

        protected override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
