using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceInscricao: ServiceBase<Inscricao>, IServiceInscricao
    {
        private readonly IRepositoryInscricao _inscricao;
        public ServiceInscricao(IRepositoryInscricao repositoryInscricao)
            :base(repositoryInscricao)
        {
            _inscricao = repositoryInscricao;
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
            var query = await _inscricao.DoListAsync(p => p.Participante!.CPF == cpf && p.Evento!.Codigo == evento);

            if (query.Count() == 0)
                return false;
            else
                return true;
        }

    }
}
