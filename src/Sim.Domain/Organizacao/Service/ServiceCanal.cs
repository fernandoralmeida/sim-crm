namespace Sim.Domain.Organizacao.Service
{
    using System;
    using System.Linq.Expressions;
    using Model;
    using Organizacao.Interfaces.Repository;
    using Organizacao.Interfaces.Service;
    public class ServiceCanal : ServiceBase<ECanal>, IServiceCanal
    {
        private readonly IRepositoryCanal _canal;
        public ServiceCanal(IRepositoryCanal repositoryCanal)
            :base(repositoryCanal)
        { _canal = repositoryCanal; }

        public async Task<ECanal> GetAsync(Guid id)
        {
            return await _canal.GetAsync(id);
        }

        public async Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null)
        {
            return await _canal.DoListAsync(filter);
        }

        public async Task<IEnumerable<(string canal, string value)>> DoListJson(string setor)
        {
            var list = await DoListAsync(s => s.Dominio!.Nome == setor);
            var canallist = new List<(string canal, string value)>();

            foreach(var item in list)
            {
                canallist.Add(new() { canal = item.Nome!, value = item.Nome! });
            }

            return canallist;
        }
    }
}
