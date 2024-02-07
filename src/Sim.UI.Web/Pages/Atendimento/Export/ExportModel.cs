namespace Sim.UI.Web.Pages.Atendimento.Export
{
    public class ExportModel
    {
        public int N { get; set; }
        public string? Data { get; set; }
        public string? Inicio { get; set; }
        public string? Termino { get; set; }
        public string? Cliente { get; set; }
        public object? Empresa { get; set; }
        public string? CNAE {get; set; }
        public string? Atividade { get; set; }
        public string? Logradouro_PF { get; set; }
        public string? Bairro_PF { get; set; }
        public string? Municipio_PF { get; set; }
        public string? Logradouro_PJ { get; set; }
        public string? Bairro_PJ { get; set; }
        public string? Municipio_PJ { get; set; }
        public string? Contato { get; set; }
        public string? Servico { get; set; }
        public string? Descricao { get; set; }
        public string? Setor { get; set; }
        public string? Canal { get; set; }
        public string? Atendente { get; set; }
    }
}
