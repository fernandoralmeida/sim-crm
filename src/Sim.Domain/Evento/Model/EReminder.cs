using Sim.Domain.Organizacao.Model;

namespace Sim.Domain.Evento.Model;

public class EReminder
{
    public enum TReminder { Privado = 0, Publico = 1 }
    public EReminder()
    { }
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Local { get; set; }
    public TReminder Visivel { get; set; }
    public DateTime Data { get; set; }
    public DateTime Data_Cadastro { get; set; }
    public string? Descricao { get; set; }
    public string? Owner { get; set; }
    public bool? Status { get; set; }
}