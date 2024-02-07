namespace Sim.Domain.Organizacao.Service
{
    using System.Linq.Expressions;
    using Model;
    using Organizacao.Interfaces.Repository;
    using Organizacao.Interfaces.Service;
    public class ServiceServico : ServiceBase<EServico>, IServiceServico
    {
        private readonly IRepositoryServico _servico;
        public ServiceServico(IRepositoryServico repositoryServico)
            :base(repositoryServico)
        {
            _servico = repositoryServico;
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
            var list = await DoListAsync(s => s.Dominio!.Nome == setor || s.Dominio.Hierarquia == EHierarquia.Secretaria);

            var servicelist = new List<(string canal, string value)>();

            foreach (var item in list.OrderBy(s => s.Nome))
            {
                servicelist.Add(new() { canal = item.Nome!, value = item.Nome! });
            }

            return servicelist;
        }
    }
}
