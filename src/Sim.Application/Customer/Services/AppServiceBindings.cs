using System.Linq.Expressions;
using Sim.Application.Customer.Interfaces;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;
using Sim.Domain.Response;

namespace Sim.Application.Customer.Services;

public class AppServiceBindings : AppServiceBase<EBindings>, IAppServiceBindings
{
    private readonly IServiceBindings _repository;

    public AppServiceBindings(IServiceBindings repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null)
        => await _repository.DoListAsync(param);

    public async Task<PagedResponse<EBindings>> DoListPaginedAsync(Expression<Func<EBindings, bool>>? param = null, int pag = 1, int itemsPerPage = 10)
    {
        return await _repository.DoListPaginedAsync(param, pag, itemsPerPage);
    }

    public async Task<EBindings> GetAsync(Guid id)
    {
        return await _repository.GetAsync(id);
    }
}