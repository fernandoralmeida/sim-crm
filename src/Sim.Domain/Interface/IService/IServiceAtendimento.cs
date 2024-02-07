namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServiceAtendimento : IServiceBase<EAtendimento>
    {
        Task<EAtendimento> GetAsync(Guid id);
        Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);
        Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam);
    }
}
