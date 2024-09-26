using System.ComponentModel.DataAnnotations;

namespace Sim.Application.Agenda.Views;

public class VReminder
{
    public enum TReminder { Privado = 0, Setorial = 1, Publico = 2 }
    public VReminder()
    { }
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Local { get; set; }
    public TReminder Visivel { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public string? Owner { get; set; }
    public bool? Status { get; set; }

}