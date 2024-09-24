using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Data.Repository;

public class RepositoryBPPRenegociacoes : RepositoryBase<ERenegociacoes>, IRepositoryRenegociacoes
{
    public RepositoryBPPRenegociacoes(ApplicationContext dbcontext) : base(dbcontext)
    {
    }

    public async Task<IEnumerable<ERenegociacoes>?> DoListAsync(Expression<Func<ERenegociacoes, bool>>? filter = null)
    {
        return await _db.RenegociacoesBPP!
                        .Include(i => i.Contrato)
                        .Where(filter ?? (p => true))
                        .AsNoTrackingWithIdentityResolution()
                        .ToListAsync();
    }

    public async Task<ERenegociacoes?> GetIdAsync(Guid id)
    {
        return await _db.RenegociacoesBPP!
                        .Include(i => i.Contrato)
                        .Where(u => u.Id == id)
                        .OrderBy(d => d.Data)
                        .FirstOrDefaultAsync();
    }
}