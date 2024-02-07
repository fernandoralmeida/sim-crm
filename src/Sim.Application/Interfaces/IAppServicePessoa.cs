using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServicePessoa : IAppServiceBase<Pessoa>
    {
        Task<Pessoa> GetAsync(Guid id);
        Task<IEnumerable<Pessoa>> DoListAsync(Expression<Func<Pessoa, bool>>? filter = null);   
        Task<IEnumerable<Pessoa>> GetLastAsync(Expression<Func<Pessoa, bool>>? filter = null, int registros = 10);
    }
}
