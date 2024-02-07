using Sim.Domain.Entity;

namespace Sim.Domain.Evento.Model;

public class EBIEventos {
    public KeyValuePair<string, int>? EventosP { get; set; }
    public KeyValuePair<string, int>? EventosR { get; set; }
    public KeyValuePair<string, int>? EventosC { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Eventos { get; set; }
    public KeyValuePair<string, int>? Participantes { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? FaixaEtaria { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? ParticipantesGenero { get; set; } 
    public KeyValuePair<string, float>? TaxaPreenchimentoParticipantes { get; set; } 
    public IEnumerable<KeyValuePair<string, int>>? EventosSetores { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? EventosMeses { get; set; }
} 