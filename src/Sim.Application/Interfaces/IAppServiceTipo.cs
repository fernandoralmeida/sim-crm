using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceTipo : IAppServiceBase<ETipo>
    {
        Task<ETipo> GetAsync(Guid id);
        Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null);
    }
}
