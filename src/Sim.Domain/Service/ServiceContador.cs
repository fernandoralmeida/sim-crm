using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceContador : ServiceBase<Contador>, IServiceContador
    {
        private readonly IRepositoryContador _contador;
        public ServiceContador(IRepositoryContador repositorycontador)
            :base(repositorycontador)
        { _contador = repositorycontador; }

        public Task<string> GetProtocoloAsync(string appuserid, string moduloname)
        {
            return _contador.GetProtocoloAsync(appuserid, moduloname);
        }
    }
}
