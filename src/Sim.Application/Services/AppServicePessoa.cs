using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServicePessoa : AppServiceBase<Pessoa>, IAppServicePessoa
    {
        private readonly IServicePessoa _pessoa;

        public AppServicePessoa(IServicePessoa pessoa) : base(pessoa)
        {
            _pessoa = pessoa;
        }

        public async Task<IEnumerable<Pessoa>> DoListAsync(Expression<Func<Pessoa, bool>>? filter = null)
        {
            return await _pessoa.DoListAsync(filter);
        }

        public async Task<Pessoa> GetAsync(Guid id)
        {
            return await _pessoa.GetAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> GetLastAsync(Expression<Func<Pessoa, bool>>? filter = null, int registros = 10)
        {
            return await _pessoa.GetLastAsync(filter, registros);
        }
    }
}
