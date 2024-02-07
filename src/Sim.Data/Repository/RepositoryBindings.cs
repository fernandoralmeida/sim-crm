using System.Linq.Expressions;
using Sim.Data.Context;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository;

public class RepositoryBindings : RepositoryBase<EBindings>, IRepositoryBindings
{
    public RepositoryBindings(ApplicationContext appcontext) : base(appcontext)
    {

    }

    public async Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null)
    {
        return await _db.Vinculos!
                .Where(param ?? (p => true))
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .OrderBy(o => o.Pessoa!.Nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
    }

    public async Task<EBindings> GetAsync(Guid id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _db.Vinculos!
                        .Include(p => p.Pessoa)
                        .Include(e => e.Empresa)
                        .FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
    }
}