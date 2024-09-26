using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{
        using Model;
    public interface IServiceReminder : IServiceBase<EReminder>
    {        
        Task<EReminder?> GetAsync(Guid id);
        Task<IEnumerable<EReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null);
    }
}
