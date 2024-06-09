using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Araintelsoftware.Data;
using Microsoft.AspNetCore.Identity;
using Araintelsoft.Services.Search;
using System.Configuration;
using Araintelsoftware.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Leer configuración desde archivo
var config = builder.Configuration;

// Connection strings
var sqlServerConfig = config.GetSection("SqlServer");

var araintelsoftConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=True;MultipleActiveResultSets=true";
var araintelsqlConnectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=True;MultipleActiveResultSets=true";

// Add services to the container.
builder.Services.AddControllersWithViews();

// AraintelsoftDBContext
builder.Services.AddDbContext<AraintelsoftDBContext>(
    options => options.UseSqlServer(araintelsoftConnectionString));

builder.Services.AddTransient<IEmailSender, EmailSender>();

// AraintelsqlContext
builder.Services.AddDbContext<AraintelsqlContext>(
    options => options.UseSqlServer(araintelsqlConnectionString));

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole<string>>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddEntityFrameworkStores<AraintelsoftDBContext>()
  .AddRoles<IdentityRole<string>>()
  .AddDefaultTokenProviders()
  .AddUserManager<UserManager<IdentityUser>>();

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