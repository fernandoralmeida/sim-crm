using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;
using static Sim.Domain.BancoPovo.Models.EContrato;

namespace Sim.Application.BancoPovo.ViewModel;

public class VMContrato {

    [Key]
    public Guid Id { get; set; }
    public int Numero { get; set; }

    [DisplayName("Data")]
    [DataType(DataType.Date)]
    public DateTime? Data { get ;set; }
    
    [Range(0, 99999.99)]
    [DataType(DataType.Currency)]
    public decimal Valor { get; set; }
    public EnSituacao Situacao { get; set; }    

    [DisplayName("Data Situação")]
    [DataType(DataType.Date)]
    public DateTime? DataSituacao { get; set; }
    public Pessoa? Cliente { get; set; }
    public Empresas? Empresa { get; set; }
    public EnPagamento Pagamento { get; set; }
    public string? Descricao { get; set; }
    public string? AppUser { get; set; }

    [DisplayName("Ultima Alteração")]
    [DataType(DataType.Date)]
    public DateTime? UltimaAlteracao { get; set; }
    public virtual ICollection<VMRenegociacoes>? Renegociacaoes { get; set; }
}