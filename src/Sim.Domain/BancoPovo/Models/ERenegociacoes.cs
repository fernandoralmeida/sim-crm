namespace Sim.Domain.BancoPovo.Models;
public class ERenegociacoes {
    public enum EnSituacao { Ativo = 0, Cancelado = 1 }
    public ERenegociacoes() {
    }
    public Guid Id { get; set; }
    public EContrato? Contrato { get; set; }
    public DateTime? Data { get; set; }
    public EnSituacao Situacao { get; set; }
}