using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Application.BancoPovo.Interfaces;
public interface IAppServiceContratos : IAppServiceBase<EContrato> {
    Task<EContrato?> GetIdAsync(Guid id);
    Task<IEnumerable<EContrato>?> DoListAsync(Expression<Func<EContrato, bool>>? filter = null);    
    Task<EReports?> DoReportsAsync(IEnumerable<EContrato> lista);
}