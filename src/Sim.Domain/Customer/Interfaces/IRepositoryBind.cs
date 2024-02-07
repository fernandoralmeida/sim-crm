using System.Linq.Expressions;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Interfaces;

public interface IRepositoryBindings : IRepositoryBase<EBindings>
{
    Task<EBindings> GetAsync(Guid id);
    Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null);
}