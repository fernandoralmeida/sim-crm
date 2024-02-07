using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceContador : AppServiceBase<Contador>, IAppServiceContador
    {
        private readonly IServiceContador _contador;
        public AppServiceContador(IServiceContador contador)
            :base(contador)
        { _contador = contador; }

        public Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            return _contador.GetProtocoloAsync(appuserid, moduloname);
        }
    }
}
