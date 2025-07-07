using System.Text.Json.Serialization;

namespace Sim.Application.RWS.Entity
{
    public class Atividade
    {

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }
}
