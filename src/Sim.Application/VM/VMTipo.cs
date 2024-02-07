using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.VM;

public class VMTipo {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Nome")]
    public string? Nome { get; set; }

    [DisplayName("Tipo")]
    public string? Tipo { get; set; } //Tipo

    [Required]
    [DisplayName("Dominio")]
    public EOrganizacao? Dominio { get; set; } //Secretaria

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}