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

var araintelsoftConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=True;MultipleActiveResultSets=true";
var araintelsqlConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=True;MultipleActiveResultSets=true";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<InterfazEmailSender>(provider => new EmailSender(builder.Configuration)); ;

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
   .AddEntityFrameworkStores<AraintelsoftDBContext>()
   .AddDefaultTokenProviders();

builder.Services.AddDbContext<AraintelsqlContext>(
    options => options.UseSqlServer(araintelsqlConnectionString));

builder.Services.AddDbContext<AraintelsoftDBContext>(
    options => options.UseSqlServer(araintelsoftConnectionString));

// Services
builder.Services.AddScoped<IBuscadorLinkedinService, BuscadorService>();

// Razor Pages
builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.Run();