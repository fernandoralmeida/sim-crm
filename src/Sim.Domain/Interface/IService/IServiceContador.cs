namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceContador : IServiceBase<Contador>
    {
        Task<string> GetProtocoloAsync(string appuserid, string moduloname);
    }
}
