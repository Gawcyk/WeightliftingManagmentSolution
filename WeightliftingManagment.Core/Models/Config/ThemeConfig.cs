using System.Text.Json.Serialization;

namespace WeightliftingManagment.Core.Models.Config
{
    public class ThemeConfig
    {
        [JsonPropertyName("Theme")]
        public string Theme { get; set; }
        [JsonPropertyName("AccentHex")]
        public string AccentHex { get; set; }

        public override bool Equals(object obj) => obj is ThemeConfig another && Theme.Equals(another.Theme) && AccentHex.Equals(another.AccentHex);

        public override int GetHashCode() => Theme.GetHashCode() + AccentHex.GetHashCode();
    }
}
