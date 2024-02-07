using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;
using Sim.Domain.Helpers;
using System.Linq.Expressions;

namespace Sim.Domain.Service
{
    public class ServiceEmpresa : ServiceBase<Empresas>, IServiceEmpresa
    {
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ServiceEmpresa(IRepositoryEmpresa repositoryEmpresa)
            :base(repositoryEmpresa)
        {
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null)
        {
            return await _repositoryEmpresa.DoListAsync(filter);
        }

        public async Task<Empresas> GetAsync(Guid id)
        {
            return await _repositoryEmpresa.GetAsync(id);
        }
    }
}
