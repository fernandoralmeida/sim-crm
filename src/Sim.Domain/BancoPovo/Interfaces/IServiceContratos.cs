using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Domain.BancoPovo.Interfaces;

public interface IServiceContratos : IServiceBase<EContrato> {
    Task<EContrato?> GetIdAsync(Guid id);
    Task<IEnumerable<EContrato>?> DoListAsync(Expression<Func<EContrato, bool>>? filter = null);
}