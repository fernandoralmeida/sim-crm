using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServiceStatusAtendimento: AppServiceBase<StatusAtendimento>, IAppServiceStatusAtendimento
    {
        private readonly IServiceStatusAtendimento _statusatendimento;

        public AppServiceStatusAtendimento(IServiceStatusAtendimento statusatendimento)
            : base(statusatendimento)
        {
            _statusatendimento = statusatendimento;
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
