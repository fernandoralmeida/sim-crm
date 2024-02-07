using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Repository
{    
    using Model;
    public interface IRepositoryEvento : IRepositoryBase<EEvento>
    {
        Task<EEvento> GetAsync(Guid id);
        Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null);
        int LastCodigo();
    }
}
