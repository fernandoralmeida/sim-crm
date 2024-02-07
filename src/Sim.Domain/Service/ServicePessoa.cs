using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;
using Sim.Domain.Helpers;
using System.Linq.Expressions;

namespace Sim.Domain.Service
{
    public class ServicePessoa : ServiceBase<Pessoa>, IServicePessoa
    {
        private readonly IRepositoryPessoa _repositoryPessoa;

        public ServicePessoa(IRepositoryPessoa repositoryPessoa) : base(repositoryPessoa)
        {
            _repositoryPessoa = repositoryPessoa;
        }

        public async Task<IEnumerable<Pessoa>> DoListAsync(Expression<Func<Pessoa, bool>>? filter = null)
        {
            return await _repositoryPessoa.DoListAsync(filter);
        }

        public async Task<Pessoa> GetAsync(Guid id)
        {
            return await _repositoryPessoa.GetAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> GetLastAsync(Expression<Func<Pessoa, bool>>? filter = null, int registros = 10)
        {
            return await _repositoryPessoa.GetLastAsync(filter, registros);
        }
    }
}
