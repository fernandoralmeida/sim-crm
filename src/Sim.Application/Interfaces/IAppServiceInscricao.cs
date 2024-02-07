using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceInscricao : IAppServiceBase<Inscricao>
    {
        Task<Inscricao> GetAsync(Guid id);
        Task<IEnumerable<Inscricao>> DoListAsync(Expression<Func<Inscricao, bool>>? filter = null);   
        Task<bool> JaInscrito(string cpf, int evento);
    }
}
