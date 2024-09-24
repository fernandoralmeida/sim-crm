using Sim.Domain.Entity;
using Sim.Application.Indicadores.VModel;
using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Indicadores.Interfaces;

public interface IAppIndicadores {
    Task<VmRAtendimentos> DoAtendimentosAsync(Expression<Func<EAtendimento, bool>>? filter = null);
    Task<VmREventos> DoEventosAsync(Expression<Func<EEvento, bool>>? param = null);
    Task<EReports> DoReportAsync(IEnumerable<EAtendimento> atendimentos, IEnumerable<EEvento> eventos);
}