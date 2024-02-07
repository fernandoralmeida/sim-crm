using System.Linq.Expressions;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceServico : IAppServiceBase<EServico>
    {
        Task<EServico> GetAsync(Guid id);
        Task<IEnumerable<(string servico, string value)>> ToListJson(string setor);
        Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null); 
    }
}
