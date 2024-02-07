using System.Linq.Expressions;

namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    using Model;
    public interface IRepositoryServico : IRepositoryBase<EServico>
    {
        Task<EServico> GetAsync(Guid id);
        Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null);
    }
}
