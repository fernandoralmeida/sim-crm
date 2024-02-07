namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServicePessoa : IServiceBase<Pessoa>
    {
        Task<Pessoa> GetAsync(Guid id);
        Task<IEnumerable<Pessoa>> DoListAsync(Expression<Func<Pessoa, bool>>? filter = null);
        Task<IEnumerable<Pessoa>> GetLastAsync(Expression<Func<Pessoa, bool>>? filter = null, int registros = 10);
    }
}
