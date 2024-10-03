using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Sim.Application.Agenda.Injector;
using Sim.Identity.IoC;
using Sim.Identity.Entity;
using Sim.Identity.Context;
using Sim.UI.Web.AutoMapper;
using Sim.IoC;
using Sim.UI.Web.Areas.Sebrae.Services;
using Sim.UI.Web.Areas.Bpp.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração de Cultura e Localização
builder.Services.AddLocalization();
var cultureInfoBR = new[] { new CultureInfo("pt-BR") };

// 2. Configuração dos Serviços de Autenticação e Identidade
builder.Services.IdentityDataBase(builder.Configuration, "ACC_DB");
builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
builder.Services.IdentityConfig();

// 3. Registro de Serviços e Dependências
builder.Services.DataBaseConfig(builder.Configuration, "APP_DB");
builder.Services.RegisterServices();
builder.Services.SebraeScoped();
builder.Services.BppScoped();
builder.Services.AddAgendaScoped();
builder.Services.AddAgendaAutoMap();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<HttpContextAccessor>();

// 4. Configuração das Páginas e Controladores
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 5. Configuração da Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = $"simcrmSession";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminGlobal", policy =>
        policy.RequireClaim("Permission", "Adm_Global"));
    options.AddPolicy("CanEditSettings", policy =>
        policy.RequireClaim("Permission", "Adm_Settings"));
    options.AddPolicy("CanManageAccounts", policy =>
        policy.RequireClaim("Permission", "Adm_Accounts"));

    options.AddPolicy("AdminOrSettings", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "Adm_Global") ||
            context.User.HasClaim("Permission", "Adm_Settings"));
    });

    options.AddPolicy("AdminOrAccounts", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "Adm_Global") ||
            context.User.HasClaim("Permission", "Adm_Settings"));
    });
});

// 6. Configuração de Cookies e Autenticação
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/contas/autenticacao/entrar");
    options.AccessDeniedPath = new PathString("/contas/acessonegado/");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// 7. Configuração de Ambiente
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// 8. Configuração de Cultura e Localização no Ambiente de Execução
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = cultureInfoBR,
    FallBackToParentCultures = false
});
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");

// 9. Middlewares e Segurança
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 10. Configuração de Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

// Mapear as páginas de áreas
// app.MapRazorPages();
// app.MapAreaControllerRoute(
//     name: "areas",
//     areaName: "Sebrae",
//     pattern: "Sebrae/{controller=Home}/{action=Index}/{id?}");

// 11. Executar o Aplicativo
await app.RunAsync();


// // Aplicar migrações automaticamente
// // using var scope = app.Services.CreateScope();
// // var _idcontext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
// // _idcontext.Database.Migrate();
// // var _appcontext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
// // _appcontext.Database.Migrate();


