using System.ComponentModel.DataAnnotations;

namespace Sim.Application.Agenda.Views;

public class VReminder
{
    public enum TReminder { Privado = 0, Publico = 1 }
    public VReminder()
    { }
    public Guid Id { get; set; }

    [Required]
    public string? Titulo { get; set; }

    [Required]
    public string? Local { get; set; }
    public TReminder Visivel { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Data { get; set; }

    public DateTime Data_Cadastro { get; set; }
    public string? Descricao { get; set; }
    public string? Owner { get; set; }
    public bool? Status { get; set; }

}