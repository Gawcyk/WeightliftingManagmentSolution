using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WeightliftingManagment.Localization.LocalizationModel
{
    public class LocalizationConverter : IMultiValueConverter
    {
        private readonly string _key;
        private readonly string _alternativeKey;
        public LocalizationConverter(string key, string alternativeKey)
        {
            _key = key;
            _alternativeKey = alternativeKey;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch (values.Length)
            {
                default:
                    // (1) CultureInfo
                    return LocalizationService.Instance.GetValue(_key) ?? LocalizationService.Instance.GetValue(_alternativeKey, false);
                case 2:
                    // (2) CultureInfo + KeySource or CultureInfo + CountSource
                    var key = values.FirstOrDefault(v => v is string);
                    // CultureInfo + KeySource
                    return LocalizationService.Instance.GetValue(key.ToString()) ?? LocalizationService.Instance.GetValue(_alternativeKey, false);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
