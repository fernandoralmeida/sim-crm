using System.Linq.Expressions;

namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceCanal : IServiceBase<ECanal>
    {
        Task<ECanal> GetAsync(Guid id);
        Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null);
        Task<IEnumerable<(string canal, string value)>> DoListJson(string setor);
    }
}
