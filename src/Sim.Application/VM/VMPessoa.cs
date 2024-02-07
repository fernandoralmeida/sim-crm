using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Customer.Models;
using Sim.Domain.Entity;

namespace Sim.Application.VM;

public class VMPessoa {
    [Key]
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [DisplayName("Nome Social")]
    public string? Nome_Social { get; set; }

    [DisplayName("Data de Nascimento")]
    [DataType(DataType.Date)]
    public DateTime Data_Nascimento { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    
    [DisplayName("Emissor")]
    public string? RG_Emissor { get; set; }
    
    [DisplayName("UF")]
    public string? RG_Emissor_UF { get; set; }
    public string? Genero { get; set; }
    
    [DisplayName("Deficiência")]
    public string? Deficiencia { get; set; }
    public string? CEP { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? UF { get; set; }
    
    [DisplayName("Celular")]
    public string? Tel_Movel { get; set; }
    
    [DisplayName("Telefone")]
    public string? Tel_Fixo { get; set; }
    public string? Email { get; set; }
    
    [DisplayName("Data do Cadastro")]
    public DateTime Data_Cadastro { get; set; }
    
    [DisplayName("Ultima Alteração")]
    public DateTime Ultima_Alteracao { get; set; }
    public bool Ativo { get; set; }
    
    public virtual ICollection<EAtendimento>? Atendimentos { get; set; }
    public virtual ICollection<Inscricao>? Inscricoes { get; set; }
    public virtual ICollection<EBindings>? Vinculos { get; set; }
}