using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Repository
{    
    using Model;
    public interface IRepositoryReminder : IRepositoryBase<EReminder>
    {
        Task<EReminder?> GetAsync(Guid id);
        Task<EReminder?> GetAsNoTrackingAsync(Guid id);
        Task<IEnumerable<EReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null);
    }
}
