using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Interfaces;
    public class AppServiceTipo : AppServiceBase<ETipo>, IAppServiceTipo
    {
        private readonly IServiceTipo _tipo;
        public AppServiceTipo(IServiceTipo tipo)
            : base(tipo)
        {
            _tipo = tipo;
        }

        public async Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null)
        {
            return await _tipo.DoListAsync(filter);
        }

        public async Task<ETipo> GetAsync(Guid id)
        {
            return await _tipo.GetAsync(id);
        }

    }
}
