
namespace Sim.Domain.Evento.Service;
using Model;
using Interfaces.Repository;
using Interfaces.Service;
using System.Linq.Expressions;

public class ServiceReminder : ServiceBase<EReminder>, IServiceReminder
{
    private readonly IRepositoryReminder _reminder;
    public ServiceReminder(IRepositoryReminder repositoryReminder)
        : base(repositoryReminder)
    {
        _reminder = repositoryReminder;
    }

    public async Task<IEnumerable<EReminder>?> DoListAsync(Expression<Func<EReminder, bool>>? filter = null)
    {
        return await _reminder.DoListAsync(filter);
    }

    public async Task<EReminder?> GetAsNoTrackingAsync(Guid id)
    {
        return await _reminder.GetAsNoTrackingAsync(id);
    }

    public async Task<EReminder?> GetAsync(Guid id)
    {
        return await _reminder.GetAsync(id);
    }
}
