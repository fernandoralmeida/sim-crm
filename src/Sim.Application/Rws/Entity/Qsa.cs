
using System.Text.Json.Serialization;

namespace Sim.Application.RWS.Entity
{
    public class Qsa
    {
        [JsonPropertyName("qual")]
        public string? Qual { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("qual_rep_legal")]
        public string? QualRepLegal { get; set; }

        [JsonPropertyName("nome_rep_legal")]
        public string? NomeRepLegal { get; set; }

        [JsonPropertyName("pais_origem")]
        public string? PaisOrigem { get; set; }
    }
}
