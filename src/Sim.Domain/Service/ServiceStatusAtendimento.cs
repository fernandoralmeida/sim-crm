using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceStatusAtendimento : ServiceBase<StatusAtendimento>, IServiceStatusAtendimento
    {
        private readonly IRepositoryStatusAtendimento _statusatendimento;
        public ServiceStatusAtendimento(IRepositoryStatusAtendimento repositoryStatusAtendimento)
            : base(repositoryStatusAtendimento) {
            _statusatendimento = repositoryStatusAtendimento;
        }

        public async Task<IEnumerable<StatusAtendimento>> DoListAsync(Expression<Func<StatusAtendimento, bool>>? filter = null)
        {
            return await _statusatendimento.DoListAsync(filter);
        }

        public async Task<StatusAtendimento> GetAsync(Guid id)
        {
            return await _statusatendimento.GetAsync(id);
        }
    }
}
