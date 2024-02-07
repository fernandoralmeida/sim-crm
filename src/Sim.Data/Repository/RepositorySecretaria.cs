using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositorySecretaria : RepositoryBase<EOrganizacao>, IRepositorySecretaria
    {
        public RepositorySecretaria(ApplicationContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<EOrganizacao>> DoListAsync(Expression<Func<EOrganizacao, bool>>? filter = null)
        {
            return await _db.Secretaria!
                    .Where(filter ?? (p => true))
                    .Include(i => i.Servicos)
                    .Include(i => i.Canais)
                    .OrderBy(o => o.Nome)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
        }

        public async Task<EOrganizacao> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Secretaria!
                                .Include(i => i.Servicos)
                                .Include(i => i.Canais)
                                .FirstOrDefaultAsync(u => u.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
