
namespace Sim.Domain.Entity
{
    public class Planner
    {
        public Planner()
        {

        }
        public Guid Id { get; set; }
        public string? Segunda { get; set; }
        public string? Terca { get; set; }
        public string? Quarta { get; set; }
        public string? Quinta { get; set; }
        public string? Sexta { get; set; }
        public string? Sabado { get; set; }
        public string? ProximaSemana { get; set; }
        public string? Prioridades { get; set; }
        public string? Anotacao { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public string? Owner_AppUser_Id { get; set; }
        public bool Ativo { get; set; }
    }
}
