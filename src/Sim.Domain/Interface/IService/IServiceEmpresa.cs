namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;

    public interface IServiceEmpresa : IServiceBase<Empresas>
    {
        Task<Empresas> GetAsync(Guid id);
        Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null);   
    }
}
