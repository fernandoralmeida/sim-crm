using System.Linq.Expressions;

namespace Sim.Domain;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task UpdateAsync(TEntity obj);
    Task RemoveAsync(TEntity obj);
}

