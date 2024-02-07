namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryEmpresa : IRepositoryBase<Empresas>
    {
        Task<Empresas> GetAsync(Guid id);
        Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null);
    }
}
