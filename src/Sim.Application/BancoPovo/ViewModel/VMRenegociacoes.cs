using static Sim.Domain.BancoPovo.Models.EContrato;

namespace Sim.Application.BancoPovo.ViewModel;
public class VMRenegociacoes {
    public Guid Id { get; set; }
    public VMContrato? Contrato { get; set; }
    public DateTime? Data { get; set; }
    public EnSituacao Situacao { get; set; }
}