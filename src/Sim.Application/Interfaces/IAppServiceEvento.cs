using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEvento : IAppServiceBase<EEvento>
    {
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetAsync(Guid id);
        Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null);

        
    }
}
