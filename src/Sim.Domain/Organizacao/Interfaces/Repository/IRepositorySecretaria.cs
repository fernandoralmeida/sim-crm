namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    using System.Linq.Expressions;
    using Model;
    public interface IRepositorySecretaria : IRepositoryBase<EOrganizacao>
    {
        Task<EOrganizacao> GetAsync(Guid id);
        Task<IEnumerable<EOrganizacao>> DoListAsync(Expression<Func<EOrganizacao, bool>>? filter = null);
    }
}
