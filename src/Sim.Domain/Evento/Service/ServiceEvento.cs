
namespace Sim.Domain.Evento.Service
{
    using Model;
    using Interfaces.Repository;
    using Interfaces.Service;
    using System.Linq.Expressions;

    public class ServiceEvento : ServiceBase<EEvento>, IServiceEvento
    {
        private readonly IRepositoryEvento _evento;
        public ServiceEvento(IRepositoryEvento repositoryEvento)
            :base(repositoryEvento)
        {
            _evento = repositoryEvento;
        }

        public async Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null)
        {
            return await _evento.DoListAsync(filter);
        }

        public async Task<EEvento> GetAsync(Guid id)
        {
            return await _evento.GetAsync(id);
        }

        public int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        /// <summary>
        /// Lista eventos classificados por mês
        /// </summary>
        /// <param name="eventos">Lista de eventos</param>
        /// <returns>Lista de eventos por mês</returns>
        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var _lista_meses = new List<(string Mes, int Qtde, IEnumerable<EEvento>)>();
                    var _eventos = new List<EEvento>();                    
                
                    for(int a = 2020; a <= (DateTime.Now.Year + 2); a++ )
                    {
                        for (int i = 1; i < 13; i++)
                        {
                            _eventos = eventos.Where(s => s.Data!.Value.Month == i && s.Data.Value.Year == a).OrderBy(o => o.Data).ToList();
                            if(_eventos.Any())
                                _lista_meses.Add((string.Format("{0} {1}", a, _eventos.FirstOrDefault()!.Data!.Value.ToString("MMM")), _eventos.Count, _eventos));
                        }

                    }
                    return _lista_meses;
                });
            }
            catch
            {
                return null!;
            }
        }

    }
}
