using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Sim.Application.RWS.Entity;

namespace Sim.Application.VM;

public class VMEmpresa
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "CNPJ requerido")]
    public string? CNPJ { get; set; }

    [DataType(DataType.Date)]
    [DisplayName("Data da Abertura")]
    public DateTime? Data_Abertura { get; set; }

    // [Required(ErrorMessage = "Nome emrpesarial requerido")]
    [DisplayName("Nome Empresarial")]
    public string? Nome_Empresarial { get; set; }

    [DisplayName("Nome Fantasia")]
    public string? Nome_Fantasia { get; set; }

    //[Required(ErrorMessage = "CNAE requerido")]
    [DisplayName("CNAE")]
    public string? CNAE_Principal { get; set; }

    //[Required(ErrorMessage = "Atividade principal requerido")]
    [DisplayName("Atividade Principal")]
    public string? Atividade_Principal { get; set; }

    //[Required(ErrorMessage = "Atividade secundária requerido")]
    [DisplayName("Atividade Secundária")]
    public string? Atividade_Secundarias { get; set; }

    //[Required(ErrorMessage = "CEP requerido")]
    public string? CEP { get; set; }

    //[Required(ErrorMessage = "Endereço requerido")]
    [DisplayName("Endereço")]
    public string? Logradouro { get; set; }

    //[Required(ErrorMessage = "Número requerido")]
    [DisplayName("Número")]
    public string? Numero { get; set; }

    public string? Complemento { get; set; }

    //[Required(ErrorMessage = "Bairro requerido")]
    [DisplayName("Bairro")]
    public string? Bairro { get; set; }

    //[Required(ErrorMessage = "Município requerido")]
    [DisplayName("Municipio")]
    public string? Municipio { get; set; }

    //[Required(ErrorMessage = "UF")]
    [DisplayName("Estado")]
    public string? UF { get; set; }

    [DisplayName("E-mail")]
    public string? Email { get; set; }

    //[Required(ErrorMessage = "Telefone requerido")]
    public string? Telefone { get; set; }

    //[Required(ErrorMessage = "Situação cadastral requerido")]
    [DisplayName("Situação Cadastral")]
    public string? Situacao_Cadastral { get; set; }

    public static VMEmpresa? FromCNPJ(CNPJ _cnpj)
    {
        if (_cnpj == null)
            return null;

        return new VMEmpresa
        {
            CNPJ = _cnpj.Cnpj,
            Data_Abertura = DateTime.TryParse(_cnpj.Data_Abertura, out var data) ? data : null,
            Nome_Empresarial = _cnpj.Nome_Empresarial,
            Nome_Fantasia = _cnpj.Nome_Fantasia,
            CNAE_Principal = _cnpj.AtividadePrincipal?.FirstOrDefault()?.Code,
            Atividade_Principal = _cnpj.AtividadePrincipal?.FirstOrDefault()?.Text,
            Atividade_Secundarias = _cnpj.AtividadesSecundarias != null ? string.Join(", ", _cnpj.AtividadesSecundarias.Select(a => $"{a.Code} - {a.Text}")) : null,
            CEP = _cnpj.Cep,
            Logradouro = _cnpj.Logradouro,
            Numero = _cnpj.Numero,
            Complemento = _cnpj.Complemento,
            Bairro = _cnpj.Bairro,
            Municipio = _cnpj.Municipio,
            UF = _cnpj.Uf,
            Email = _cnpj.Email,
            Telefone = _cnpj.Telefone,
            Situacao_Cadastral = _cnpj.Situacao_Cadastral
        };
    }
}