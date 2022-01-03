using System.Text.Json.Serialization;

namespace WeightliftingManagment.Core.Models.Config
{
    public class SinclaireParams
    {
        [JsonPropertyName("ParamA")]
        public double ParamA { get; set; }
        [JsonPropertyName("ParamB")]
        public double ParamB { get; set; }
    }

}
