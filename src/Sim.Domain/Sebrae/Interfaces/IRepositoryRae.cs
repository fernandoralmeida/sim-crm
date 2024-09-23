using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Model;

namespace Sim.Domain.Sebrae.Interfaces;

public interface IRepositoryRae
{
    Task AddAsync(EAtendimento obj);
    Task UpdateAsync(RaeSebrae obj);
    Task RemoveAsync(RaeSebrae obj);
    Task<EAtendimento?> GetAtendimentoByIdAsync(EAtendimento obj);
    Task<RaeSebrae?> GetRaeByIdAsync(RaeSebrae obj);
    Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);
}