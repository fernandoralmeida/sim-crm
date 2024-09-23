using Sim.Application;
using Sim.Application.Sebrae.Interfaces;
using Sim.Application.Sebrae.Services;
using Sim.Data;
using Sim.Data.Repository;
using Sim.Domain;
using Sim.Domain.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;
using Sim.Domain.Sebrae.Services;

namespace Sim.UI.Web.Areas.Sebrae.Services;

public static class SebraeService
{
    public static void SebraeScoped(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryRae, RepositorySebraeRae>();
        services.AddScoped<IServiceRae, ServiceRae>();
        services.AddScoped<IAppServiceRae, AppServiceRae>();

        services.AddScoped<IRepositoryBase<ESimples>, RepositoryBase<ESimples>>();
        services.AddScoped<IRepositorySimples, RepositorySebraeSimples>();
        services.AddScoped<IServiceBase<ESimples>, ServiceBase<ESimples>>();
        services.AddScoped<IServiceSimples, ServiceSimples>();
        services.AddScoped<IAppServiceBase<ESimples>, AppServiceBase<ESimples>>();
        services.AddScoped<IAppServiceSimples, AppServiceSimples>();
    }
}