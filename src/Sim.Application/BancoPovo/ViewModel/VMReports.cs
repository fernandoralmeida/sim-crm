using System.ComponentModel.DataAnnotations;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Application.BancoPovo.ViewModel;

public class VMReports {

    [DataType(DataType.Date)]
    public DateTime? DataInicial { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataFinal { get; set; }

    public EReports? Relatorios { get; set; }
}