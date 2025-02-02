using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using aradork.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar los servicios al contenedor
builder.Services.AddDbContext<aradorkDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AradorkConnection")));

// Agregar otros servicios
builder.Services.AddControllersWithViews();

// Configurar Kestrel para escuchar en el puerto 5003 (Loopback)
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Loopback, 5003);  // Cambiar el puerto aquí
});

// Habilitar sesiones (por ejemplo, para mantener el estado del usuario)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Habilitar Razor Pages y Controladores
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Construir la aplicación
var app = builder.Build();

// Configuración de la tubería de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();  // Habilitar sesiones
app.UseAuthorization();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
    name: "searchdorks",
    pattern: "AragonDorks/searchDorks",
    defaults: new { controller = "AragonDorks", action = "SearchDorks" });

app.MapControllers();
app.MapRazorPages();

app.Run();