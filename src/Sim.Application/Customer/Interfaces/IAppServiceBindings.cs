using System.Linq.Expressions;
using Sim.Domain.Customer.Models;
using Sim.Domain.Response;

namespace Sim.Application.Customer.Interfaces;

public interface IAppServiceBindings : IAppServiceBase<EBindings>
{
    Task<EBindings> GetAsync(Guid id);
    Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null);
    Task<PagedResponse<EBindings>> DoListPaginedAsync(Expression<Func<EBindings, bool>>? param = null, int pag = 1, int itemsPerPage = 10);
}