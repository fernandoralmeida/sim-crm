using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServiceInscricao : AppServiceBase<Inscricao>, IAppServiceInscricao
    {
        private readonly IServiceInscricao _inscricao;
        public AppServiceInscricao(IServiceInscricao serviceInscricao)
            :base(serviceInscricao)
        {
            _inscricao = serviceInscricao;
        }

        public async Task<IEnumerable<Inscricao>> DoListAsync(Expression<Func<Inscricao, bool>>? filter = null)
        {
            return await _inscricao.DoListAsync(filter);
        }

        public async Task<Inscricao> GetAsync(Guid id)
        {
            return await _inscricao.GetAsync(id);
        }

        public async Task<bool> JaInscrito(string cpf, int evento)
        {
            return await _inscricao.JaInscrito(cpf, evento);
        }
    }
}
