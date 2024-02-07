namespace Sim.Domain.Evento.Service
{
    using Model;
    using Interfaces.Repository;
    using Interfaces.Service;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ServiceTipos : ServiceBase<ETipo>, IServiceTipo
    {
        private readonly IRepositoryTipo _repositoryTipo;

        public ServiceTipos(IRepositoryTipo repositoryTipo)
            :base(repositoryTipo)
        {
            _repositoryTipo = repositoryTipo;
        }

        public async Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null)
        {
            return await _repositoryTipo.DoListAsync(filter);
        }

        public async Task<ETipo> GetAsync(Guid id)
        {
            return await _repositoryTipo.GetAsync(id);
        }

        public int LastCodigo()
        {
            return 0;
        }
    }
}
