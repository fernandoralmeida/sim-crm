namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryContador : IRepositoryBase<Contador>
    {
        Task<string> GetProtocoloAsync(string appuserid, string moduloname);

    }
}
