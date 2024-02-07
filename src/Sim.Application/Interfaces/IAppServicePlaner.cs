using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServicePlaner : IAppServiceBase<Planner>
    {
        Task<Planner> GetAsync(Guid id);
        Task<IEnumerable<Planner>> DoListAsync(Expression<Func<Planner, bool>>? filter = null);   
    }
}
