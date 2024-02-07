using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceContador : IAppServiceBase<Contador>
    {
        Task<string> GetProtocoloAsync(string appuserid, string moduloname);
    }
}
