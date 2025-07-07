using System.Text.Json.Serialization;

namespace Sim.Application.RWS.Entity
{
    public class CNPJ
    {
        [JsonPropertyName("atividade_principal")]
        public List<Atividade>? AtividadePrincipal { get; set; }

        [JsonPropertyName("data_situacao")]
        public string? Data_Situacao_Cadastral { get; set; }

        [JsonPropertyName("complemento")]
        public string? Complemento { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome_Empresarial { get; set; }

        [JsonPropertyName("uf")]
        public string? Uf { get; set; }

        [JsonPropertyName("telefone")]
        public string? Telefone { get; set; }

        [JsonPropertyName("atividades_secundarias")]
        public List<Atividade>? AtividadesSecundarias { get; set; }

        [JsonPropertyName("qsa")]
        public List<Qsa>? Qsa { get; set; }

        [JsonPropertyName("situacao")]
        public string? Situacao_Cadastral { get; set; }

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        [JsonPropertyName("logradouro")]
        public string? Logradouro { get; set; }

        [JsonPropertyName("numero")]
        public string? Numero { get; set; }

        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("municipio")]
        public string? Municipio { get; set; }

        [JsonPropertyName("abertura")]
        public string? Data_Abertura { get; set; }

        [JsonPropertyName("natureza_juridica")]
        public string? Natureza_Juridica { get; set; }

        [JsonPropertyName("cnpj")]
        public string? Cnpj { get; set; }

        [JsonPropertyName("ultima_atualizacao")]
        public DateTimeOffset UltimaAtualizacao { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("tipo")]
        public string? Tipo { get; set; }

        [JsonPropertyName("fantasia")]
        public string? Nome_Fantasia { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("efr")]
        public string? Ente_Federativo_Resp { get; set; }

        [JsonPropertyName("motivo_situacao")]
        public string? Motivo_Situacao_Cadastral { get; set; }

        [JsonPropertyName("situacao_especial")]
        public string? Situacao_Especial { get; set; }

        [JsonPropertyName("data_situacao_especial")]
        public string? Data_Situacao_Especial { get; set; }

        [JsonPropertyName("capital_social")]
        public decimal Capital_Social { get; set; }

        [JsonPropertyName("porte")]
        public string? Porte { get; set; }

        //[JsonPropertyName("extra")]
        //public Extra Extra { get; set; }
    }
}
