namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryInscricao : IRepositoryBase<Inscricao>
    {
        Task<Inscricao> GetAsync(Guid id);
        Task<IEnumerable<Inscricao>> DoListAsync(Expression<Func<Inscricao, bool>>? filter = null);
    }
}
