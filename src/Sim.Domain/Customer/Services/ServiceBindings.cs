using System.Linq.Expressions;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Services;

public class ServiceBindings : ServiceBase<EBindings>, IServiceBindings
{
    private readonly IRepositoryBindings _repository;

    public ServiceBindings(IRepositoryBindings repository)
    : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null)
    {
        return await _repository.DoListAsync(param);
    }

    public async Task<EBindings> GetAsync(Guid id)
    {
        return await _repository.GetAsync(id);
    }
}