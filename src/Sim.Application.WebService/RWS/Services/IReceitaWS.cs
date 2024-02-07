using Sim.Application.WebService.RWS.Entity;

namespace Sim.Application.WebService.RWS.Services
{
    public interface IReceitaWS
    {
        Task<CNPJ> ConsultarCPNJAsync(string cnpj);
    }
}
