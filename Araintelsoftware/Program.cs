using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Araintelsoftware.Data;
using Microsoft.AspNetCore.Identity;
using Araintelsoft.Services.Search;
using System.Configuration;
using Araintelsoftware.Areas.Identity.Data;
using Araintelsoftware.Services.EmailSender;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Leer configuración desde archivo
var config = builder.Configuration;

// Connection strings
var sqlServerConfig = config.GetSection("SqlServer");
var araintelsoftConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User ID={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";
var araintelsqlConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User ID={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";

// Add services to the container
// Servicios de Identity
builder.Services.AddIdentity<SampleUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<AraintelsoftDBContext>()
.AddDefaultTokenProviders()
.AddUserManager<UserManager<SampleUser>>();

// Servicios de Email
builder.Services.AddSingleton<InterfazEmailSender>(provider => new EmailSender(builder.Configuration));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Servicios de DbContext
builder.Services.AddDbContext<AraintelsqlContext>(
    options => options.UseSqlServer(araintelsqlConnectionString));
builder.Services.AddDbContext<AraintelsoftDBContext>(
    options => options.UseSqlServer(araintelsoftConnectionString));

// Servicios de Buscador
builder.Services.AddScoped<IBuscadorLinkedinService, BuscadorService>();

// Razor Pages
builder.Services.AddRazorPages();

// Controllers with Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rutas de la aplicación
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "search",
    pattern: "Agenda/Search",
    defaults: new { controller = "Agenda", action = "Search" });

app.MapRazorPages();

// Create the database and apply migrations
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AraintelsoftDBContext>();
    context.Database.Migrate();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapAreaControllerRoute(
        name: "Identity",
        areaName: "Identity",
        pattern: "Identity/{controller=Account}/{action=Login}/{id?}");
});

app.Run();