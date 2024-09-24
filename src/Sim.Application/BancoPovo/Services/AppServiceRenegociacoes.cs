using Sim.Domain.BancoPovo.Models;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.BancoPovo.Services;
public class AppServiceRenegociacoes : AppServiceBase<ERenegociacoes>, IAppServiceRenegociacoes
{
    private readonly IServiceRenegociacoes _renegociacoes;
    public AppServiceRenegociacoes(IServiceRenegociacoes serviceBase) : base(serviceBase)
    {
        _renegociacoes = serviceBase;
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