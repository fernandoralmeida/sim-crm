using System.Linq.Expressions;

namespace Sim.Application;
public interface IAppServiceBase<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task UpdateAsync(TEntity obj);
    Task RemoveAsync(TEntity obj);
}

