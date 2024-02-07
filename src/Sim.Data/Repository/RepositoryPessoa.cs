using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryPessoa : RepositoryBase<Pessoa>, IRepositoryPessoa
    {
        public RepositoryPessoa(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<Pessoa>> DoListAsync(Expression<Func<Pessoa, bool>>? filter = null)
        {
            return await _db.Pessoa!
                .Where(filter ?? (p => true))
                .Include(i => i.Atendimentos)
                .Include(i => i.Inscricoes)
                .Include(i => i.Vinculos)
                .AsNoTrackingWithIdentityResolution()
                .OrderByDescending(o => o.Ultima_Alteracao)
                .ToListAsync();
        }

        public async Task<Pessoa> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Pessoa!
                            .Include(i => i.Atendimentos)
                            .Include(i => i.Inscricoes)
                            .Include(i => i.Vinculos)
                            .FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Pessoa>> GetLastAsync(Expression<Func<Pessoa, bool>>? filter = null, int registros = 10)
        {
            return await _db.Pessoa!
                .Where(filter ?? (p => true))
                .Include(i => i.Atendimentos)
                .Include(i => i.Inscricoes)
                .Include(i => i.Vinculos)
                .AsNoTrackingWithIdentityResolution()
                .OrderByDescending(o => o.Ultima_Alteracao)
                .Take(registros)
                .ToListAsync();
        }
    }
}
