using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;

namespace Sim.Domain.Sebrae.Services;

public class ServiceRae : IServiceRae
{
    protected readonly IRepositoryRae? _repository;

    public ServiceRae(IRepositoryRae repository) { _repository = repository; }

    public async Task AddAsync(EAtendimento obj)
    {
        var _rae = await _repository!.GetAtendimentoByIdAsync(obj);

        if (_rae != null)
        {
            _rae.Sebrae = new RaeSebrae()
            {
                Id = new Guid(),
                RAE = obj.Sebrae!.RAE
            };

            await _repository.AddAsync(_rae);
        }
    }

    public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null)
    {
        return await _repository!.DoListAsync(filter);
    }

    public async Task RemoveAsync(RaeSebrae obj)
    {
        await _repository!.RemoveAsync(obj);
    }

    public async Task UpdateAsync(RaeSebrae obj)
    {
        var _rae = await _repository!.GetRaeByIdAsync(obj);
        _rae!.RAE = obj.RAE;
        await _repository!.UpdateAsync(_rae);
    }
}