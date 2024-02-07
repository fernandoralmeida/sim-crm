using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{    
    using Model;
    public interface IServiceParceiro : IServiceBase<EParceiro>
    {
        Task<EParceiro> GetAsync(Guid id);
        Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null);
    }
}
