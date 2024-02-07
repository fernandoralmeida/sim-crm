using Newtonsoft.Json;

namespace Sim.Application.WebService.RWS.Entity
{
    public class Qsa
    {
        [JsonProperty("qual")]
        public string? Qual { get; set; }

        [JsonProperty("nome")]
        public string? Nome { get; set; }

        [JsonProperty("qual_rep_legal", NullValueHandling = NullValueHandling.Ignore)]
        public string? QualRepLegal { get; set; }

        [JsonProperty("nome_rep_legal", NullValueHandling = NullValueHandling.Ignore)]
        public string? NomeRepLegal { get; set; }

        [JsonProperty("pais_origem", NullValueHandling = NullValueHandling.Ignore)]
        public string? PaisOrigem { get; set; }
    }
}
