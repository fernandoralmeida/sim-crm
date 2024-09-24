using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Domain.BancoPovo.Services;

public class ServiceRenegociacoes : ServiceBase<ERenegociacoes>, IServiceRenegociacoes
{
    private readonly IRepositoryRenegociacoes _renegociacoes;

    public ServiceRenegociacoes(IRepositoryRenegociacoes repositoryRenegociacoes) : base(repositoryRenegociacoes)
    {
        _renegociacoes = repositoryRenegociacoes;
    }

    public async Task<IEnumerable<ERenegociacoes>?> DoListAsync(Expression<Func<ERenegociacoes, bool>>? filter = null)
    {
       return await _renegociacoes.DoListAsync(filter);
    }

    public async Task<ERenegociacoes?> GetIdAsync(Guid id)
    {
        return await _renegociacoes.GetIdAsync(id);
    }
}