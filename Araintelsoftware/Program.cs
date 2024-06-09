using Araintelsoft.Services.Search;
using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Leer configuración desde archivo
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Acceder a los secrets
var sqlServerConfig = config.GetSection("SqlServer");

builder.Services.AddDbContext<AraintelsqlContext>(
    options => options.UseSqlServer($"server={sqlServerConfig["Server"]};database={sqlServerConfig["Database"]};user={sqlServerConfig["User"]};password={sqlServerConfig["Password"]};"));

builder.Services.AddScoped<IBuscadorLinkedinService, BuscadorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "search",
    pattern: "Agenda/Search",
    defaults: new { controller = "Agenda", action = "Search" });

app.Run();