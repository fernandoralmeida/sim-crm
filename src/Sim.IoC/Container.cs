using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Sim.Domain;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Service;
using Sim.Domain.Organizacao.Interfaces.Service;
using Sim.Domain.Organizacao.Interfaces.Repository;

using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Service;
using Sim.Domain.Evento.Interfaces.Service;
using Sim.Domain.Evento.Interfaces.Repository;

using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Service;

using Sim.Application;
using Sim.Application.Interfaces;
using Sim.Application.Services;

using Sim.Application.WebService.RWS.Services;

using Sim.Data;
using Sim.Data.Context;
using Sim.Data.Repository;

using Sim.Domain.Customer.Models;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Services;
using Sim.Application.Customer.Interfaces;
using Sim.Application.Customer.Services;

using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.Services;
using Sim.Application.WebService.RFB.Interfaces;
using Sim.Application.WebService.RFB.Services;

namespace Sim.IoC
{
    public static class Container
    {


        public static void DataBaseConfig(this IServiceCollection services, IConfiguration config, string connection)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            services.AddScoped<ApplicationContext>();
            //
            services.AddScoped<IAppServiceBase<Pessoa>, AppServiceBase<Pessoa>>();
            services.AddScoped<IAppServicePessoa, AppServicePessoa>();

            services.AddScoped<IServiceBase<Pessoa>, ServiceBase<Pessoa>>();
            services.AddScoped<IServicePessoa, ServicePessoa>();

            services.AddScoped<IRepositoryBase<Pessoa>, RepositoryBase<Pessoa>>();
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();

            //
            services.AddScoped<IAppServiceBase<Empresas>, AppServiceBase<Empresas>>();
            services.AddScoped<IAppServiceEmpresa, AppServiceEmpresa>();

            services.AddScoped<IServiceBase<Empresas>, ServiceBase<Empresas>>();
            services.AddScoped<IServiceEmpresa, ServiceEmpresa>();

            services.AddScoped<IRepositoryBase<Empresas>, RepositoryBase<Empresas>>();
            services.AddScoped<IRepositoryEmpresa, RepositoryEmpresa>();

            //
            services.AddScoped<IAppServiceBase<EAtendimento>, AppServiceBase<EAtendimento>>();
            services.AddScoped<IAppServiceAtendimento, AppServiceAtendimento>();

            services.AddScoped<IServiceBase<EAtendimento>, ServiceBase<EAtendimento>>();
            services.AddScoped<IServiceAtendimento, ServiceAtendimento>();

            services.AddScoped<IRepositoryBase<EAtendimento>, RepositoryBase<EAtendimento>>();
            services.AddScoped<IRepositoryAtendimento, RepositoryAtendimento>();

            // Status Atendimento
            services.AddScoped<IAppServiceBase<StatusAtendimento>, AppServiceBase<StatusAtendimento>>();
            services.AddScoped<IAppServiceStatusAtendimento, AppServiceStatusAtendimento>();

            services.AddScoped<IServiceBase<StatusAtendimento>, ServiceBase<StatusAtendimento>>();
            services.AddScoped<IServiceStatusAtendimento, ServiceStatusAtendimento>();

            services.AddScoped<IRepositoryBase<StatusAtendimento>, RepositoryBase<StatusAtendimento>>();
            services.AddScoped<IRepositoryStatusAtendimento, RepositoryStatusAtendimento>();
            //
            services.AddScoped<IAppServiceBase<EOrganizacao>, AppServiceBase<EOrganizacao>>();
            services.AddScoped<IAppServiceSecretaria, AppServiceSecretaria>();

            services.AddScoped<IServiceBase<EOrganizacao>, ServiceBase<EOrganizacao>>();
            services.AddScoped<IServiceSecretaria, ServiceSecretaria>();

            services.AddScoped<IRepositoryBase<EOrganizacao>, RepositoryBase<EOrganizacao>>();
            services.AddScoped<IRepositorySecretaria, RepositorySecretaria>();

            //
            services.AddScoped<IAppServiceBase<EServico>, AppServiceBase<EServico>>();
            services.AddScoped<IAppServiceServico, AppServiceServico>();

            services.AddScoped<IServiceBase<EServico>, ServiceBase<EServico>>();
            services.AddScoped<IServiceServico, ServiceServico>();

            services.AddScoped<IRepositoryBase<EServico>, RepositoryBase<EServico>>();
            services.AddScoped<IRepositoryServico, RepositoryServico>();

            //
            services.AddScoped<IAppServiceBase<EEvento>, AppServiceBase<EEvento>>();
            services.AddScoped<IAppServiceEvento, AppServiceEvento>();

            services.AddScoped<IServiceBase<EEvento>, ServiceBase<EEvento>>();
            services.AddScoped<IServiceEvento, ServiceEvento>();

            services.AddScoped<IRepositoryBase<EEvento>, RepositoryBase<EEvento>>();
            services.AddScoped<IRepositoryEvento, RepositoryEvento>();

            //
            services.AddScoped<IAppServiceBase<ECanal>, AppServiceBase<ECanal>>();
            services.AddScoped<IAppServiceCanal, AppServiceCanal>();

            services.AddScoped<IServiceBase<ECanal>, ServiceBase<ECanal>>();
            services.AddScoped<IServiceCanal, ServiceCanal>();

            services.AddScoped<IRepositoryBase<ECanal>, RepositoryBase<ECanal>>();
            services.AddScoped<IRepositoryCanal, RepositoryCanal>();

            //
            services.AddScoped<IAppServiceBase<Contador>, AppServiceBase<Contador>>();
            services.AddScoped<IAppServiceContador, AppServiceContador>();

            services.AddScoped<IServiceBase<Contador>, ServiceBase<Contador>>();
            services.AddScoped<IServiceContador, ServiceContador>();

            services.AddScoped<IRepositoryBase<Contador>, RepositoryBase<Contador>>();
            services.AddScoped<IRepositoryContador, RepositoryContador>();

            //
            services.AddScoped<IAppServiceBase<Inscricao>, AppServiceBase<Inscricao>>();
            services.AddScoped<IAppServiceInscricao, AppServiceInscricao>();

            services.AddScoped<IServiceBase<Inscricao>, ServiceBase<Inscricao>>();
            services.AddScoped<IServiceInscricao, ServiceInscricao>();

            services.AddScoped<IRepositoryBase<Inscricao>, RepositoryBase<Inscricao>>();
            services.AddScoped<IRepositoryInscricao, RepositoryInscricao>();

            //
            services.AddScoped<IAppServiceBase<Planner>, AppServiceBase<Planner>>();
            services.AddScoped<IAppServicePlaner, AppServicePlaner>();

            services.AddScoped<IServiceBase<Planner>, ServiceBase<Planner>>();
            services.AddScoped<IServicePlaner, ServicePlaner>();

            services.AddScoped<IRepositoryBase<Planner>, RepositoryBase<Planner>>();
            services.AddScoped<IRepositoryPlaner, RepositoryPlaner>();

            //
            services.AddScoped<IAppServiceBase<EParceiro>, AppServiceBase<EParceiro>>();
            services.AddScoped<IAppServiceParceiro, AppServiceParceiro>();

            services.AddScoped<IServiceBase<EParceiro>, ServiceBase<EParceiro>>();
            services.AddScoped<IServiceParceiro, ServiceParceiros>();

            services.AddScoped<IRepositoryBase<EParceiro>, RepositoryBase<EParceiro>>();
            services.AddScoped<IRepositoryParceiro, RepositoryParceiro>();

            //
            services.AddScoped<IAppServiceBase<ETipo>, AppServiceBase<ETipo>>();
            services.AddScoped<IAppServiceTipo, AppServiceTipo>();

            services.AddScoped<IServiceBase<ETipo>, ServiceBase<ETipo>>();
            services.AddScoped<IServiceTipo, ServiceTipos>();

            services.AddScoped<IRepositoryBase<ETipo>, RepositoryBase<ETipo>>();
            services.AddScoped<IRepositoryTipo, RepositoryTipo>();

            //
            services.AddScoped<IAppServiceBase<EBindings>, AppServiceBase<EBindings>>();
            services.AddScoped<IAppServiceBindings, AppServiceBindings>();
            services.AddScoped<IServiceBase<EBindings>, ServiceBase<EBindings>>();
            services.AddScoped<IServiceBindings, ServiceBindings>();
            services.AddScoped<IRepositoryBase<EBindings>, RepositoryBase<EBindings>>();
            services.AddScoped<IRepositoryBindings, RepositoryBindings>();
            //
            services.AddScoped<IReceitaWS, ReceitaWS>();
            services.AddScoped<IServiceRFB, ServiceRFB>();
            //
            services.AddScoped<IAppIndicadores, AppIndicadores>();

            

        }
    }
}
