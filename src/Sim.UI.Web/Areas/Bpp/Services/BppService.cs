using Sim.Application;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.Services;
using Sim.Data;
using Sim.Data.Repository;
using Sim.Domain;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;
using Sim.Domain.BancoPovo.Services;

namespace Sim.UI.Web.Areas.Bpp.Services;
public static class BppService
{
    public static void BppScoped(this IServiceCollection services)
    {
        //
        services.AddScoped<IAppServiceBase<EContrato>, AppServiceBase<EContrato>>();
        services.AddScoped<IAppServiceContratos, AppServiceContrato>();

        services.AddScoped<IServiceBase<EContrato>, ServiceBase<EContrato>>();
        services.AddScoped<IServiceContratos, ServiceContratos>();

        services.AddScoped<IRepositoryBase<EContrato>, RepositoryBase<EContrato>>();
        services.AddScoped<IRepositoryContratos, RepositoryBPPContratos>();
        
        //
        services.AddScoped<IAppServiceBase<ERenegociacoes>, AppServiceBase<ERenegociacoes>>();
        services.AddScoped<IAppServiceRenegociacoes, AppServiceRenegociacoes>();

        services.AddScoped<IServiceBase<ERenegociacoes>, ServiceBase<ERenegociacoes>>();
        services.AddScoped<IServiceRenegociacoes, ServiceRenegociacoes>();

        services.AddScoped<IRepositoryBase<ERenegociacoes>, RepositoryBase<ERenegociacoes>>();
        services.AddScoped<IRepositoryRenegociacoes, RepositoryBPPRenegociacoes>();
    }
}