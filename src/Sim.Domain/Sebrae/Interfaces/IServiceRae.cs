using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Model;

namespace Sim.Domain.Sebrae.Interfaces;

public interface IServiceRae 
{
    Task AddAsync(EAtendimento obj);
    Task UpdateAsync(RaeSebrae obj);
    Task RemoveAsync(RaeSebrae obj);
    Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);
}