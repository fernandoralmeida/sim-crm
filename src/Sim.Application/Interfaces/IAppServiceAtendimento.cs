using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceAtendimento : IAppServiceBase<EAtendimento>
    {
        Task<EAtendimento> GetAsync(Guid id);
        Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);
        Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam);
    }
}
