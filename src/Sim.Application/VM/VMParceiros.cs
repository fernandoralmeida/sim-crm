using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.VM;

public class VMParceiros {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Parceiro")]
    public string? Nome { get; set; }

    [DisplayName("Secretaria")]
    public EOrganizacao? Dominio { get; set; } //Secretaria

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}