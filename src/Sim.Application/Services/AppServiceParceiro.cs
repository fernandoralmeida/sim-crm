using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using System.Linq.Expressions;
    using Interfaces;
    public class AppServiceParceiro : AppServiceBase<EParceiro>, IAppServiceParceiro
    {
        private readonly IServiceParceiro _parceiro;
        public AppServiceParceiro(IServiceParceiro parceiro)
            : base(parceiro)
        {
            _parceiro = parceiro;
        }

        public async Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null)
        {
            return await _parceiro.DoListAsync(filter);
        }

        public async Task<EParceiro> GetAsync(Guid id)
        {
            return await _parceiro.GetAsync(id);
        }
    }
}
