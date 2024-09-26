using System.Linq.Expressions;
using Sim.Application.Agenda.Views;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Agenda.Interfaces;

public interface IAppServiceReminder 
{
    Task AddNewAsync(VReminder obj);
    Task RemoveAsync(VReminder obj);
    Task UpdateAsync(VReminder obj);
    Task<VReminder?> GetAsync(Guid id);
    Task<IEnumerable<VReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null);
}
