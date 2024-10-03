using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace Sim.Data.Repository;
public class RepositoryReminder : RepositoryBase<EReminder>, IRepositoryReminder
{
    public RepositoryReminder(ApplicationContext dbContext)
        : base(dbContext)
    {

    }

    public async Task<IEnumerable<EReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null)
    {
        return await _db.Reminders!
                .Where(filter ?? (p => true))
                .OrderByDescending(o => o.Data)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
    }

    public async Task<EReminder?> GetAsNoTrackingAsync(Guid id)
    {
        return await _db.Reminders!
                        .AsNoTrackingWithIdentityResolution()
                        .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<EReminder?> GetAsync(Guid id)
    {
        return await _db.Reminders!
                    .FirstOrDefaultAsync(s => s.Id == id);
    }

}

