using System.Linq.Expressions;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;

namespace Sim.Application.Sebrae.Services;

public class AppServiceSimples : AppServiceBase<ESimples>, IAppServiceSimples
{
    private readonly IServiceSimples _simples;
    public AppServiceSimples(IServiceSimples simples) : base(simples)
    {
        _simples = simples;
    }

    public async Task<IEnumerable<ESimples>?> DoListAsync(Expression<Func<ESimples, bool>>? filter = null)
        => await _simples.DoListAsync(filter);

    public async Task<ESimples?> GetByIdAsync(Guid id)
    {
        return await _simples.GetByIdAsync(id);
    }
}

