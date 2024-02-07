using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Interfaces;
    public class AppServiceEvento : AppServiceBase<EEvento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public static async Task<EBIEventos> DoBIEventosAsync(IEnumerable<EEvento> lista)
        {
            return await Task.Run(() => {
                var _list = new EBIEventos
                {
                    EventosP = new KeyValuePair<string, int>("Proposto", lista.Count()),
                    EventosR = new KeyValuePair<string, int>("Realizados", lista.Where(s => s.Situacao != EEvento.ESituacao.Cancelado && s.Data <= DateTime.Now).Count()),
                    EventosC = new KeyValuePair<string, int>("Cancelados", lista.Where(s => s.Situacao == EEvento.ESituacao.Cancelado).Count())
                };

                var _nome_eventos = new List<KeyValuePair<string, int>>();
                foreach (var e in lista.GroupBy(g => g.Tipo)
                                        .OrderByDescending(o => o.Count())) {
                    _nome_eventos.Add(new KeyValuePair<string, int>(e.Key!, e.Count()));
                }
                _list.Eventos = _nome_eventos;

                var _partc = 0;
                foreach (var p in lista) {
                    _partc += p.Inscritos!.Count();
                }
                _list.Participantes = new KeyValuePair<string, int>("Inscritos", _partc);

                var _list_faixa = new List<KeyValuePair<string, int>>();
                var _faixa_etaria = new List<string>();
                var _genero = new List<string>();
                var _list_genero = new List<KeyValuePair<string, int>>();
                var _txparticipante = 0;
                
                foreach (var f in lista.Where(s => s.Situacao != EEvento.ESituacao.Cancelado)) {
                    foreach (var i in f.Inscritos!) {
                        if(i.Presente)
                            _txparticipante++;
                        _genero.Add(i.Participante!.Genero!);
                        
                        var d1 = new DateTime(i.Participante.Data_Nascimento!.Value.Year,
                                            i.Participante.Data_Nascimento.Value.Month,
                                            i.Participante.Data_Nascimento.Value.Day);

                        var d2 = new DateTime(f.Data!.Value.Year,
                                            f.Data.Value.Month,
                                            f.Data.Value.Day);

                        var _faixa = (d2.Subtract(d1).TotalDays) / 365;
                        if (_faixa > 15 && _faixa < 21)
                            _faixa_etaria.Add("16 -> 20 anos");
                        else if (_faixa > 20 && _faixa < 31)
                             _faixa_etaria.Add("21 -> 30 anos");
                        else if (_faixa > 30 && _faixa < 41)
                            _faixa_etaria.Add("31 -> 40 anos");
                        else if (_faixa > 40 && _faixa < 51)
                            _faixa_etaria.Add("41 -> 50 anos");
                        else if (_faixa > 50 && _faixa < 61)
                            _faixa_etaria.Add("51 -> 60 anos");
                        else if (_faixa > 60 && _faixa < 71)
                            _faixa_etaria.Add("61 -> 70 anos");
                        else if (_faixa > 70)
                            _faixa_etaria.Add("71 anos ou mais");
                    }
                }

                foreach (var item in _faixa_etaria.GroupBy(g => g)
                                                    .OrderByDescending(o => o.Count())) {
                    _list_faixa.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
                }
                _list.FaixaEtaria = _list_faixa;

                foreach (var item in _genero.GroupBy(g => g)
                                                    .OrderByDescending(o => o.Count())) {
                    _list_genero.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
                }
                _list.ParticipantesGenero = _list_genero;

                float _tx = _txparticipante;
                float _p = _partc;
                float _r_txpart = _tx / _p * 100F;
                _list.TaxaPreenchimentoParticipantes = new KeyValuePair<string, float>("Presentes", _r_txpart);

                var _add_setor = new List<KeyValuePair<string, int>>();
                foreach (var item in lista
                                        .Where(s => s.Situacao != EEvento.ESituacao.Cancelado)
                                        .GroupBy(g => g.Owner)
                                        .OrderByDescending(o => o.Count())) {
                    _add_setor.Add(new KeyValuePair<string, int>(item.Key!, item.Count()));
                }
                _list.EventosSetores = _add_setor;

                var _add_month = new List<KeyValuePair<string, int>>();
                foreach (var item in lista
                                        .Where(s => s.Situacao != EEvento.ESituacao.Cancelado)
                                        .OrderBy(o => o.Data)
                                        .GroupBy(g => g.Data!.Value.ToString("MMM"))) {
                    _add_month.Add(new KeyValuePair<string, int>(item.Key, item.Count()));                    
                }
                _list.EventosMeses = _add_month;

                return _list;
            });
        }

        public async Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null)
        {
            return await _evento.DoListAsync(filter);
        }

        public async Task<EEvento> GetAsync(Guid id)
        {
            return await _evento.GetAsync(id);
        }

        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos)
        {
            return await _evento.ListEventosPorMesAsync(eventos);
        }
    }
}
