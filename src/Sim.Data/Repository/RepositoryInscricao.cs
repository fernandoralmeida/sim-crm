using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryInscricao : RepositoryBase<Inscricao>, IRepositoryInscricao
    {
        public RepositoryInscricao(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<IEnumerable<Inscricao>> DoListAsync(Expression<Func<Inscricao, bool>>? filter = null)
        {
            return await _db.Inscricao!
                                .Where(filter ?? (p => true))
                                .Include(e => e.Evento)
                                .Include(p => p.Participante)
                                .Include(t => t.Empresa)
                                .OrderBy(o => o.Participante!.Nome)
                                .AsNoTrackingWithIdentityResolution()
                                .ToListAsync();
        }

        public async Task<Inscricao> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Inscricao!
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
