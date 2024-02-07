using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryPlaner : RepositoryBase<Planner>, IRepositoryPlaner
    {
        public RepositoryPlaner(ApplicationContext dbContext)
            : base(dbContext)
        {        }

        public async Task<IEnumerable<Planner>> DoListAsync(Expression<Func<Planner, bool>>? filter = null)
        {
            var _query = _db.Planner!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<Planner> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Planner!.Where(p => p.Id == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
