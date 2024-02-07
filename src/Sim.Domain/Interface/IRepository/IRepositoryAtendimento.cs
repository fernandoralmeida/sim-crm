namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryAtendimento : IRepositoryBase<EAtendimento>
    {
        Task<EAtendimento> GetAsync(Guid id);
        Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);  
        Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam);      
    }
}
