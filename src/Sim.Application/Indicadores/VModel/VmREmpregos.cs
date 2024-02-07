namespace Sim.Application.Indicadores.VModel;

public class VmREmpregos
{
    public IEnumerable<VmChart>? Ativos { get; set; }
    public IEnumerable<VmChart>? Contratados { get; set; }
    public IEnumerable<VmChart>? Vagas { get; set; }
}