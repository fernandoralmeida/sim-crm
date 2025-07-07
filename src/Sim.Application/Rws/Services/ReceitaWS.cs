using System.Net.Http.Json;
using Sim.Application.RWS.Entity;
using Sim.Application.RWS.Functions;

namespace Sim.Application.RWS.Services
{
    public class ReceitaWS : IReceitaWS
    {
        private static readonly string ReceitaWSApi = "https://www.receitaws.com.br/v1/cnpj/{0}";
        private readonly HttpClient _httpClient;

        public ReceitaWS()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CNPJ> ConsultarCPNJAsync(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || !Validate.CNPJ(cnpj))
                throw new ArgumentException("CNPJ Invalido.");

            try
            {
                var result = await _httpClient
                .GetFromJsonAsync<CNPJ>(string.Format(ReceitaWSApi, cnpj)) ??
                throw new ArgumentException("CNPJ not found or invalid response.");

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
    }
}
