using Sim.Domain.Entity;
using Sim.Domain.Organizacao.Model;

namespace Sim.Domain.Evento.Model
{
    public class EEvento
    {
        public enum ESituacao { Ativo = 1, Finalizado = 2, Cancelado = 3 }
        public enum EFormato { Presencial = 1, OnLine = 2 }
        public EEvento()
        { }
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Nome { get; set; }
        public string? Formato { get; set; }
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        public string? Owner { get; set; }
        public string? Parceiro { get; set; }
        public int Lotacao { get; set; }
        public ESituacao? Situacao { get; set; }
        public virtual EOrganizacao? Dominio { get; set; }
        public virtual ICollection<Inscricao>? Inscritos { get; set; }

        public int Inscricoes()
        {
            return Inscritos == null ? 0 : Inscritos.Count;
        }
        public int Vagas()
        {
            return Inscritos == null ? Lotacao : Lotacao - Inscritos.Count;
        }

        public bool EventoBySituacao(EEvento evento, ESituacao situacao)
        {
            return evento.Situacao == situacao;
        }
    }
}
