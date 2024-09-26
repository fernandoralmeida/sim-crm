using Microsoft.Extensions.DependencyInjection;
using Sim.Application.Agenda.AutoMap;
using Sim.Application.Agenda.Interfaces;
using Sim.Application.Agenda.Services;
using Sim.Data;
using Sim.Data.Repository;
using Sim.Domain;
using Sim.Domain.Evento.Interfaces.Repository;
using Sim.Domain.Evento.Interfaces.Service;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Service;

namespace Sim.Application.Agenda.Injector;

public static class InjectorExtensions
{
    public static void AddAgendaScoped(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryBase<EReminder>, RepositoryBase<EReminder>>();
        services.AddScoped<IRepositoryReminder, RepositoryReminder>();
        services.AddScoped<IServiceBase<EReminder>, ServiceBase<EReminder>>();
        services.AddScoped<IServiceReminder, ServiceReminder>();

        services.AddScoped<IAppServiceReminder, AppServiceReminder>();
    }

    public static void AddAgendaAutoMap(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ModelView));
    }

    
}

