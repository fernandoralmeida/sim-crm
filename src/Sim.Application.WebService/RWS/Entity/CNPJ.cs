using Newtonsoft.Json;

namespace Sim.Application.WebService.RWS.Entity
{
    public class CNPJ
    {
        [JsonProperty("atividade_principal")]
        public List<Atividade>? AtividadePrincipal { get; set; }

        [JsonProperty("data_situacao")]
        public string? Data_Situacao_Cadastral { get; set; }

        [JsonProperty("complemento")]
        public string? Complemento { get; set; }

        [JsonProperty("nome")]
        public string? Nome_Empresarial { get; set; }

        [JsonProperty("uf")]
        public string? Uf { get; set; }

        [JsonProperty("telefone")]
        public string? Telefone { get; set; }

        [JsonProperty("atividades_secundarias")]
        public List<Atividade>? AtividadesSecundarias { get; set; }

        [JsonProperty("qsa")]
        public List<Qsa>? Qsa { get; set; }

        [JsonProperty("situacao")]
        public string? Situacao_Cadastral { get; set; }

        [JsonProperty("bairro")]
        public string? Bairro { get; set; }

        [JsonProperty("logradouro")]
        public string? Logradouro { get; set; }

        [JsonProperty("numero")]
        public string? Numero { get; set; }

        [JsonProperty("cep")]
        public string? Cep { get; set; }

        [JsonProperty("municipio")]
        public string? Municipio { get; set; }

        [JsonProperty("abertura")]
        public string? Data_Abertura { get; set; }

        [JsonProperty("natureza_juridica")]
        public string? Natureza_Juridica { get; set; }

        [JsonProperty("cnpj")]
        public string? Cnpj { get; set; }

        [JsonProperty("ultima_atualizacao")]
        public DateTimeOffset UltimaAtualizacao { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("tipo")]
        public string? Tipo { get; set; }

        [JsonProperty("fantasia")]
        public string? Nome_Fantasia { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("efr")]
        public string? Ente_Federativo_Resp { get; set; }

        [JsonProperty("motivo_situacao")]
        public string? Motivo_Situacao_Cadastral { get; set; }

        [JsonProperty("situacao_especial")]
        public string? Situacao_Especial { get; set; }

        [JsonProperty("data_situacao_especial")]
        public string? Data_Situacao_Especial { get; set; }

        [JsonProperty("capital_social")]
        public decimal Capital_Social { get; set; }

        [JsonProperty("porte")]
        public string? Porte { get; set; }

        //[JsonProperty("extra")]
        //public Extra Extra { get; set; }
    }
}
