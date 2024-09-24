using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Application.BancoPovo.Interfaces;
public interface IAppServiceRenegociacoes : IAppServiceBase<ERenegociacoes> {
    Task<ERenegociacoes?> GetIdAsync(Guid id);
    Task<IEnumerable<ERenegociacoes>?> DoListAsync(Expression<Func<ERenegociacoes, bool>>? filter = null);
}