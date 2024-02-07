using Sim.Domain.Evento.Model;

namespace Sim.Domain.Entity
{
    public class Inscricao
    {
        public Inscricao()
        {

        }
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string? AplicationUser_Id { get; set; }
        public DateTime? Data_Inscricao { get; set; }
        public bool Presente { get; set; }

        public virtual Pessoa? Participante { get; set; }
        public virtual Empresas? Empresa { get; set; }
        public virtual EEvento? Evento { get; set; }
    }
}
