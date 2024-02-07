using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceStatusAtendimento : IAppServiceBase<StatusAtendimento>
    {
        Task<StatusAtendimento> GetAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> DoListAsync(Expression<Func<StatusAtendimento, bool>>? filter = null); 
    }
}
