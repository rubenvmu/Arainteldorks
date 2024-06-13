using Araintelsoft.Services.Search;
using Araintelsoftware.Areas.Identity.Data;
using Araintelsoftware.Data;
using Araintelsoftware.Models;
using Araintelsoftware.Services.EmailSender;
using Araintelsoftware.Services.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var KeyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultUrl").Value!);
var azureCredential = new DefaultAzureCredential();

builder.Configuration.AddAzureKeyVault(KeyVaultUrl, azureCredential);

var config = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

var cs = builder.Configuration.GetSection("araintelsqldb").Value;

builder.Services.AddControllers();

builder.Services.AddDbContext<AraintelsoftDBContext>(options => options.UseSqlServer(cs));
builder.Services.AddDbContext<AraintelsqlContext>(options => options.UseSqlServer(cs));
builder.Services.AddDbContext<AragonDorksContext>(options => options.UseSqlServer(cs));

builder.Services.AddIdentity<SampleUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<AraintelsoftDBContext>()
.AddDefaultTokenProviders()
.AddUserManager<UserManager<SampleUser>>()
.AddSignInManager<SignInManager<SampleUser>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
});

builder.Services.AddSingleton<InterfazEmailSender>(provider =>
{
    if (config != null)
    {
        return new EmailSender(config);
    }
    else
    {
        throw new ArgumentNullException(nameof(config), "La configuración es necesaria para crear una instancia de EmailSender");
    }
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});

builder.Services.AddScoped<IBuscadorLinkedinService, BuscadorService>();
builder.Services.AddScoped<IBuscadorAragondorks, BuscadorAragondorks>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "search",
    pattern: "Agenda/Search",
    defaults: new { controller = "Agenda", action = "Search" });

app.MapControllerRoute(
    name: "searchdorks",
    pattern: "AragonDorks/searchDorks",
    defaults: new { controller = "AragonDorks", action = "SearchDorks" });

app.MapRazorPages();

if (app.Services != null)
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
    {
        if (serviceScope != null)
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<AragonDorksContext>();
        }
    }
}

app.MapControllers();
app.MapRazorPages();
app.MapAreaControllerRoute(
    name: "Identity",
    areaName: "Identity",
    pattern: "Identity/{controller=Account}/{action=Login}/{id?}");

app.Run();