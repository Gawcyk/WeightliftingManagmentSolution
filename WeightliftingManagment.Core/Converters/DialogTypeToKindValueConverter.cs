using System.Globalization;

using WeightliftingManagment.Core.BaseType;
using WeightliftingManagment.Core.Dialogs;

namespace WeightliftingManagment.Core.Converters
{
    public class DialogTypeToKindValueConverter : BaseMarkupValueConverter
    {
        public string Alert { get; set; } = "\xE801";

        public string Info { get; set; } = "\xE802";

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (DialogType)value switch {
            DialogType.Alert => Alert,
            DialogType.Information => Info,
            _ => null,
        };

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
