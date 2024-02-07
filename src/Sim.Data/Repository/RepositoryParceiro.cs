using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{

    public class RepositoryParceiro : RepositoryBase<EParceiro>, IRepositoryParceiro
    {
        public RepositoryParceiro(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null) {
            var _query = _db.Parceiro!.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(s => s.Dominio)
                    .OrderBy(s => s.Nome)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<EParceiro> GetAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Parceiro!.Include(s => s.Dominio).Where(u => u.Id == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
