namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServiceInscricao : IServiceBase<Inscricao>
    {
        Task<Inscricao> GetAsync(Guid id);
        Task<IEnumerable<Inscricao>> DoListAsync(Expression<Func<Inscricao, bool>>? filter = null);   
        Task<bool> JaInscrito(string cpf, int evento);
    }
}
