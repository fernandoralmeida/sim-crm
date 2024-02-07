namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryPlaner : IRepositoryBase<Planner>
    {
        Task<Planner> GetAsync(Guid id);
        Task<IEnumerable<Planner>> DoListAsync(Expression<Func<Planner, bool>>? filter = null);
    }
}
