using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryServico : RepositoryBase<EServico>, IRepositoryServico
    {
        public RepositoryServico(ApplicationContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null)
        {
            var _query = _db.Servico!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .Include(i => i.Dominio)
                    .OrderBy(o => o.Nome)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<EServico> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Servico!.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

    }

}
