using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Interfaces;

    public class AppServiceCanal : AppServiceBase<ECanal>, IAppServiceCanal
    {
        private readonly IServiceCanal _canal;
        public AppServiceCanal(IServiceCanal canal)
            :base(canal)
        { _canal = canal; }

        public async Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null)
        {
            return await _canal.DoListAsync(filter);
        }

        public async Task<IEnumerable<(string canal, string value)>> DoListJson(string setor)
        {
            return await _canal.DoListJson(setor);
        }

        public async Task<ECanal> GetAsync(Guid id)
        {
            return await _canal.GetAsync(id);
        }
    }
}
