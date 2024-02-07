using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Interfaces;
    public class AppServiceServico : AppServiceBase<EServico>, IAppServiceServico
    {
        private readonly IServiceServico _servico;
        public AppServiceServico(IServiceServico servico)
            :base(servico)
        {
            _servico = servico;
        }

        public async Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null)
        {
            return await _servico.DoListAsync(filter);
        }

        public async Task<EServico> GetAsync(Guid id)
        {
            return await _servico.GetAsync(id);
        }

        public async Task<IEnumerable<(string servico, string value)>> ToListJson(string setor)
        {
            return await _servico.ToListJson(setor);
        }
    }
}
