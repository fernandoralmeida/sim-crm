using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.VM;
public class VMSecretaria {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [Required]
    [DisplayName("Acrônimo")]
    public string? Acronimo { get; set; }
    
    [Required]
    [DisplayName("Nível")]
    public EHierarquia? Hierarquia { get; set; } //Prefeitura
    
    [DisplayName("Dominio")]
    public Guid? Dominio { get; set; } //Prefeitura

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }

    public virtual ICollection<VMCanal>? Canais { get; set; }
    public virtual ICollection<VMServicos>? Servicos { get; set; }
}