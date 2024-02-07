namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServiceStatusAtendimento : IServiceBase<StatusAtendimento>
    {
        Task<StatusAtendimento> GetAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> DoListAsync(Expression<Func<StatusAtendimento, bool>>? filter = null);   
    }
}
