
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;

namespace Sim.Domain.Organizacao.Model
{
    public enum EHierarquia { Matriz = 0, Secretaria = 1, Setor = 2 }
    public class EOrganizacao
    {
        public EOrganizacao() { }

        public EOrganizacao(Guid id, string nome, string acronimo,
            EHierarquia? hierarquia, Guid? dominio, bool ativo)
        {
            Id = id;
            Nome = nome;
            Acronimo = acronimo;
            Hierarquia = hierarquia;
            Dominio = dominio;
            Ativo = ativo;
        }
        public Guid Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Acronimo { get; private set; }
        public EHierarquia? Hierarquia { get; private set; }
        public Guid? Dominio { get; private set; } //
        public bool Ativo { get; set; }
        public virtual ICollection<ECanal>? Canais { get; set; }
        public virtual ICollection<EServico>? Servicos { get; set; }
        public virtual ICollection<EAtendimento>? Atendimentos { get; set; }
        public virtual ICollection<EEvento>? Eventos { get; set; }
        public virtual ICollection<EParceiro>? Parceiros { get; set; }
        public virtual ICollection<ETipo>? Tipos { get; set; }

        public bool IsMatriz(EOrganizacao obj)
        {
            return obj.Hierarquia == EHierarquia.Matriz;
        }
        public bool IsSecretaria(EOrganizacao obj)
        {
            return obj.Hierarquia == EHierarquia.Secretaria;
        }
        public bool IsSetor(EOrganizacao obj)
        {
            return obj.Hierarquia == EHierarquia.Setor;
        }

        public bool IsSecOfOrganizacao(EOrganizacao obj, Guid dominio)
        {
            return obj.Hierarquia == EHierarquia.Secretaria && obj.Dominio == dominio;
        }

        public bool IsSetorOfSecretaria(EOrganizacao obj, Guid dominio)
        {
            return obj.Hierarquia == EHierarquia.Setor && obj.Dominio == dominio || (obj.Id == dominio);
        }
        public bool IsSetorOfMatriz(EOrganizacao obj, Guid dominio)
        {
            return obj.Hierarquia == EHierarquia.Matriz && obj.Dominio == dominio;
        }

    }

}
