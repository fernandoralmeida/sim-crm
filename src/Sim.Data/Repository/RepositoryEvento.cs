using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace Sim.Data.Repository
{
    public class RepositoryEvento : RepositoryBase<EEvento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null)
        {
            return await _db.Evento!
                    .Where(filter ?? (p => true))
                    .Include(i => i.Inscritos!).ThenInclude(i => i.Participante!)
                    .Include(i => i.Inscritos!).ThenInclude(i => i.Empresa!)
                    .Include(d => d.Dominio)
                    .OrderByDescending(o => o.Data)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
        }

        public async Task<EEvento> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Evento!
                        .Include(i => i.Inscritos!).ThenInclude(i => i.Participante!)
                        .Include(i => i.Inscritos!).ThenInclude(i => i.Empresa!)
                        .Include(d => d.Dominio)
                        .FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public int LastCodigo()
        {
            var cod = _db.Evento!
                            .OrderBy(c => c.Codigo)
                            .AsNoTrackingWithIdentityResolution()
                            .LastOrDefault()?.Codigo;

            return cod ?? 0;
        }
    }
}
