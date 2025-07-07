using Sim.Application.RWS.Entity;

namespace Sim.Application.RWS.Services
{
    public interface IReceitaWS
    {
        Task<CNPJ> ConsultarCPNJAsync(string cnpj);
    }
}
