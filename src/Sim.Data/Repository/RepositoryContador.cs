using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryContador : RepositoryBase<Contador>, IRepositoryContador
    {
        public RepositoryContador(ApplicationContext dbContext)
            :base(dbContext)
        {   }

        public async Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            var _protocolo = $"{DateTime.Now:yyyy}-{DateTime.Now.DayOfYear:000}-{DateTime.Now:HHmmss.ff}";

            var str = string.Empty;
            var t = Task.Run(() =>
            {
                var p = new Contador()
                {
                    Numero = _protocolo,
                    Modulo = moduloname,
                    AppUserId = appuserid,
                    Data = DateTime.Now
                };

                _db.Contador!.Add(p);
                _db.SaveChanges();

                var protocolo = _db.Contador.Where(s => s.AppUserId == appuserid && s.Data == p.Data).OrderByDescending(d => d.Data).LastOrDefault();

                str = protocolo!.Numero?.ToString();
            });

            await t;

           return str;
        }

    }
}
