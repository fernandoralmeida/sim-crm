using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Atendimento.ViewModel;

namespace Sim.Application.Cliente.ViewModel;

public class VmCliente {

    [Key]
    public Guid Id { get; set; }

    [DisplayName("CPF/ CNPJ")]
    public string? Documento { get; set; }

    [DisplayName("Nome/ Razão")]
    public string? NomeRazao { get; set; }

    [DisplayName("Tel./ Email")]
    public string? Contato { get; set; }

    [DisplayName("Ultimos Atendimentos")]
    public virtual ICollection<VmAtendimento>? Atendimentos { get; set; }
}