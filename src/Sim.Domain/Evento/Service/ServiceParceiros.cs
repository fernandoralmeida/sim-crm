namespace Sim.Domain.Evento.Service
{
    using Model;
    using Evento.Interfaces.Repository;
    using Evento.Interfaces.Service;
    using System.Linq.Expressions;

    public class ServiceParceiros : ServiceBase<EParceiro>, IServiceParceiro
    {
        private readonly IRepositoryParceiro _repositoryParceiro;

        public ServiceParceiros(IRepositoryParceiro repositoryParceiro)
            : base(repositoryParceiro)
        {
            _repositoryParceiro = repositoryParceiro;
        }

        public async Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null)
        {
            return await _repositoryParceiro.DoListAsync(filter);
        }

        public async Task<EParceiro> GetAsync(Guid id)
        {
            return await _repositoryParceiro.GetAsync(id);
        }
    }
}
