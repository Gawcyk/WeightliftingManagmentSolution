using System.Globalization;
using System.Windows.Media;

using WeightliftingManagment.Core.BaseType;
using WeightliftingManagment.Core.Dialogs;

namespace WeightliftingManagment.Core.Converters
{
    public class DialogTypeToSolidColorBrushValueConverter : BaseMarkupValueConverter
    {
        /// <summary>
        /// Wartość domyślna Green
        /// </summary>
        public SolidColorBrush AlertBrush { get; set; } = new SolidColorBrush(Colors.Red);

        /// <summary>
        /// Wartość domyślna Red
        /// </summary>
        public SolidColorBrush InfoBrush { get; set; } = new SolidColorBrush(Colors.Green);

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((DialogType)value) switch {
            DialogType.Alert => AlertBrush,
            DialogType.Information => InfoBrush,
            _ => new SolidColorBrush(Colors.Transparent),
        };

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
