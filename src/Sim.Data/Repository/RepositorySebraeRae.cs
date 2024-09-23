using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;

namespace Sim.Data.Repository;

public class RepositorySebraeRae : IRepositoryRae
{
    protected readonly ApplicationContext _dbContext;
    public RepositorySebraeRae(ApplicationContext dbContext)
    { _dbContext = dbContext; }
    public async Task AddAsync(EAtendimento obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null)
    {
        return await _dbContext.Atendimento!
                    //.Where(a => a.Status == "Finalizado")
                    //.Where(a => a.Ativo == true)
                    .Include(p => p.Pessoa)
                    .Include(e => e.Empresa)
                    .Include(e => e.Dominio)
                    .Include(s => s.Sebrae)
                    .Where(filter ?? (p => true))
                    .OrderBy(o => o.Data)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
    }

    public async Task<EAtendimento?> GetAtendimentoByIdAsync(EAtendimento obj)
    {
        return await _dbContext.Atendimento!
                        .Include(p => p.Pessoa)
                        .Include(e => e.Empresa)
                        .Include(e => e.Dominio)
                        .Include(s => s.Sebrae)
                        .Where(s => s.Id == obj.Id)
                        .FirstOrDefaultAsync();
    }

    public async Task<RaeSebrae?> GetRaeByIdAsync(RaeSebrae obj)
    {
        return await _dbContext.Set<RaeSebrae>().FindAsync(obj.Id);
    }

    public async Task RemoveAsync(RaeSebrae obj)
    {
        _dbContext.Set<RaeSebrae>().Remove(obj);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RaeSebrae obj)
    {
        _dbContext.Entry(obj).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}