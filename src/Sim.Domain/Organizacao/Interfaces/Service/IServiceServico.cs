using System.Linq.Expressions;

namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceServico : IServiceBase<EServico>
    {
        Task<EServico> GetAsync(Guid id);
        Task<IEnumerable<(string servico, string value)>> ToListJson(string setor);
        Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null);        
    }
}
