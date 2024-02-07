using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEmpresa : IAppServiceBase<Empresas>
    {
        Task<Empresas> GetAsync(Guid id);
        Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null);   
    }
}
