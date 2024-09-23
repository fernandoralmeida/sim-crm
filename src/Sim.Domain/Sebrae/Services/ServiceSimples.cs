using Sim.Domain.Sebrae.Model;
using Sim.Domain.Sebrae.Interfaces;
using System.Linq.Expressions;

namespace Sim.Domain.Sebrae.Services;

public class ServiceSimples : ServiceBase<ESimples>, IServiceSimples
{
    private readonly IRepositorySimples _repository;

    public ServiceSimples(IRepositorySimples repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ESimples>> DoListAsync(Expression<Func<ESimples, bool>>? filter = null)
        => await _repository.DoListAsync(filter);

    public async Task<ESimples?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}