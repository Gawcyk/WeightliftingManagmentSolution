using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeightliftingManagment.Localization.LocalizationModel
{
    public class LocalizationEntry
    {
        public string Value { get; }

        public LocalizationEntry(string value)
        {
            Value = value;
        }
    }
}
