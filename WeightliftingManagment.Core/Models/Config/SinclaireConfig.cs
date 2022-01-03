using System.Text.Json.Serialization;

namespace WeightliftingManagment.Core.Models.Config
{
    public class SinclaireConfig
    {
        [JsonPropertyName("Men")]
        public SinclaireParams Men { get; set; }
        [JsonPropertyName("Women")]
        public SinclaireParams Women { get; set; }
    }

}
