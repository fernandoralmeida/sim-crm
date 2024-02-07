namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServicePlaner : IServiceBase<Planner>
    {
        Task<Planner> GetAsync(Guid id);
        Task<IEnumerable<Planner>> DoListAsync(Expression<Func<Planner, bool>>? filter = null);   
    }
}
