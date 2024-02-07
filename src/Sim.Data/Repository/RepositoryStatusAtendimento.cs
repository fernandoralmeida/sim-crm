using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryStatusAtendimento : RepositoryBase<StatusAtendimento>, IRepositoryStatusAtendimento
    {
        public RepositoryStatusAtendimento(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<StatusAtendimento>> DoListAsync(Expression<Func<StatusAtendimento, bool>>? filter = null)
        {
            var _query = _db.StatusAtendimento!.AsQueryable();

            if (filter != null)
                _query = _query
                    .Where(filter)
                    .OrderBy(o => o.UnserName)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<StatusAtendimento> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.StatusAtendimento!.FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
