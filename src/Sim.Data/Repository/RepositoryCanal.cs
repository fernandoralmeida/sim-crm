using Sim.Data.Context;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryCanal : RepositoryBase<ECanal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>>? filter = null)
        {
            return await _db.Canal!
                    .Where(filter ?? (p => true))
                    .Include(s => s.Dominio)
                    .OrderBy(s => s.Nome)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
        }

        public async Task<ECanal> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Canal!
                .Include(s => s.Dominio)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}

