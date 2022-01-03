using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeightliftingManagment.Core.Models.Config
{
    public class AppConfig
    {
        [JsonPropertyName("configurationsFolder")]
        public string ConfigurationsFolder { get; set; }

        [JsonPropertyName("appConfigFileName")]
        public string AppConfigFileName { get; set; }

        [JsonPropertyName("themeConfigFileName")]
        public string ThemeConfigFileName { get; set; }

        [JsonPropertyName("sinclaireConfigFileName")]
        public string SinclaireConfigFileName { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("languageFolder")]
        public string LanguageFolder { get; set; }

        [JsonPropertyName("languageFileName")]
        public string LanguageFileName { get; set; }
    }
}
