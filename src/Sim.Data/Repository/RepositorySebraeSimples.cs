using Sim.Domain.Sebrae.Model;
using Sim.Domain.Sebrae.Interfaces;
using System.Linq.Expressions;
using Sim.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository;

public class RepositorySebraeSimples : RepositoryBase<ESimples>, IRepositorySimples
{
    public RepositorySebraeSimples(ApplicationContext dbContext)
    : base(dbContext)
    { }

    public async Task<IEnumerable<ESimples>> DoListAsync(Expression<Func<ESimples, bool>>? filter = null)
    {

        return await _db.Simples!
                        .Include(i => i.Empresa)
                        .Where(filter ?? (p => true))
                        .ToListAsync();
    }

    public async Task<ESimples?> GetByIdAsync(Guid id)
    {
        return await _db.Simples!.Include(i => i.Empresa).Where(s => s.Id == id).SingleOrDefaultAsync();
    }
}