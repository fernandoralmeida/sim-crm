using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceParceiro : IAppServiceBase<EParceiro>
    {
        Task<EParceiro> GetAsync(Guid id);
        Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null);
    }
}
