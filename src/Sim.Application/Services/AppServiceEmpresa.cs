using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
   
    public class AppServiceEmpresa : AppServiceBase<Empresas>, IAppServiceEmpresa
    {
        private readonly IServiceEmpresa _empresa;

        public AppServiceEmpresa(IServiceEmpresa empresa)
            :base(empresa)
        {
            _empresa = empresa;
        }

        public async Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null)
        {
            return await _empresa.DoListAsync(filter);
        }

        public async Task<Empresas> GetAsync(Guid id)
        {
            return await _empresa.GetAsync(id);
        }
    }
}
