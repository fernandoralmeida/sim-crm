using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sim.Identity.Context;
using Sim.Identity.Interfaces;
using Sim.Identity.Repository;
using Sim.Identity.Policies;

namespace Sim.Identity.IoC;
public static class Container
{

    public static void IdentityDataBase(this IServiceCollection services, IConfiguration config, string connection)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString(connection));
        });
    }

    public static void IdentityConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddScoped<IServiceUser, RepositoryUser>();

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 5;
        });
    }

    public static void AuthorizationPolices(this IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy(PolicyExtensions.IsAdminGlobal, policy =>
                policy.RequireClaim(PolicyTypes.Permission, PolicyTypes.Adm_Global));


            options.AddPolicy(PolicyExtensions.IsAdminSettings, policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(PolicyTypes.Permission, PolicyTypes.Adm_Global) ||
                    context.User.HasClaim(PolicyTypes.Permission, PolicyTypes.Adm_Settings));
            });

            options.AddPolicy(PolicyExtensions.IsAdminAccounts, policy =>
            {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(PolicyTypes.Permission, PolicyTypes.Adm_Global) ||
                    context.User.HasClaim(PolicyTypes.Permission, PolicyTypes.Adm_Account));
            });
        });
    }
}