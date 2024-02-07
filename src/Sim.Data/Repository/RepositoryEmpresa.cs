using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{

    public class RepositoryEmpresa : RepositoryBase<Empresas>, IRepositoryEmpresa
    {
        public RepositoryEmpresa(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<Empresas>> DoListAsync(Expression<Func<Empresas, bool>>? filter = null)
        {
            return await _db.Empresa!
                                .Where(filter ?? (p => true))
                                .Include(i => i.Vinculos)
                                .Include(i => i.Atendimentos)
                                .Include(i => i.Inscricoes)
                                .OrderByDescending(o => o.Data_Abertura)
                                .AsNoTrackingWithIdentityResolution()
                                .ToListAsync();
        }

        public async Task<Empresas> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Empresa!
                                .Include(i => i.Vinculos)
                                .Include(i => i.Atendimentos)
                                .Include(i => i.Inscricoes)
                                .FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
