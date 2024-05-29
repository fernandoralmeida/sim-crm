using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Sim.Identity.IoC;
using Sim.Identity.Entity;
using Sim.Identity.Context;
using Sim.UI.Web.AutoMapper;
using Sim.IoC;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
var cultureInfoBR = new[] { new CultureInfo("pt-BR") };

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.IdentityDataBase(builder.Configuration, "ACC_DB");
builder.Services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();
builder.Services.IdentityConfig();

builder.Services.DataBaseConfig(builder.Configuration, "APP_DB");
builder.Services.RegisterServices();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = $"simcrmSession";
    });

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = new PathString("/contas/autenticacao/entrar");
        options.AccessDeniedPath = new PathString("/contas/acessonegado/");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
    app.UseDeveloperExceptionPage();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = cultureInfoBR,
    FallBackToParentCultures = false
});

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");

// Aplicar migrações automaticamente
// using var scope = app.Services.CreateScope();
// var _idcontext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
// _idcontext.Database.Migrate();
// var _appcontext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
// _appcontext.Database.Migrate();

app.UseSession();
// app.UseMiddleware<SessionIdleTimeoutMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always
});

app.UseRouting();
//app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

await app.RunAsync();
