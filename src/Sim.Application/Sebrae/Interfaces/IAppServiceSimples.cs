using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Model;
using System.Linq.Expressions;

namespace Sim.Application.Sebrae.Interfaces;

public interface IAppServiceSimples : IAppServiceBase<ESimples>
{
    Task<ESimples?> GetByIdAsync(Guid id);
    Task<IEnumerable<ESimples>?> DoListAsync(Expression<Func<ESimples, bool>>? filter = null);
}