using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{
        using Model;
    public interface IServiceEvento : IServiceBase<EEvento>
    {
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetAsync(Guid id);
        Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null);
    }
}
