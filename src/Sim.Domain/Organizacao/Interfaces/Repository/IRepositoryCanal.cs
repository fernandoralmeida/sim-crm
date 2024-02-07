using System.Linq.Expressions;

namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    
    using Model;
    public interface IRepositoryCanal : IRepositoryBase<ECanal>
    {
        Task<ECanal> GetAsync(Guid id);
        Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null);
    }
}
