using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{

    public class AppServiceAtendimento : AppServiceBase<EAtendimento>, IAppServiceAtendimento
    {
        private readonly IServiceAtendimento _atendimento;
        public AppServiceAtendimento(IServiceAtendimento atendimento)
            : base(atendimento)
        {
            _atendimento = atendimento;
        }

        public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null)
        {
            return await _atendimento.DoListAsync(filter);
        }

        public async Task<EAtendimento> GetAsync(Guid id)
        {
            return await _atendimento.GetAsync(id);
        }

        public async Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam)
        {
            return await _atendimento.ListParamAsync(lparam);
        }
    }
}
