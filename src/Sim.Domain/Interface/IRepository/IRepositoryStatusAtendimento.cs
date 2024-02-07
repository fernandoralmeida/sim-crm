namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryStatusAtendimento : IRepositoryBase<StatusAtendimento>
    {
        Task<StatusAtendimento> GetAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> DoListAsync(Expression<Func<StatusAtendimento, bool>>? filter = null);
    }
}
