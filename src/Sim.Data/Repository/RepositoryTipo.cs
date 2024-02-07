using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryTipo : RepositoryBase<ETipo>, IRepositoryTipo
    {
        public RepositoryTipo(ApplicationContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null)
        {
            var _query = _db.Tipos!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .Include(s => s.Dominio)
                    .OrderBy(o => o.Nome)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<ETipo> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Tipos!
                            .Include(i => i.Dominio)
                            .FirstOrDefaultAsync(u => u.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
