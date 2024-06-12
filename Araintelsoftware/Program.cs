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

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new VisualStudioCredential());
var config = builder.Configuration;

var sqlServerConfig = config.GetSection("SqlServer");

var araintelsoftConnectionString = $"Server={config["SqlServer:Server"]};Database={config["SqlServer:Database"]};User ID={config["SqlServer:User"]};Password={config["SqlServer:Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";
var araintelsqlConnectionString = $"Server={config["SqlServer:Server"]};Database={config["SqlServer:Database"]};User ID={config["SqlServer:User"]};Password={config["SqlServer:Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";
var AragonDorksContextConnectionString = $"Server={config["SqlServer:Server"]};Database={config["SqlServer:Database"]};User ID={config["SqlServer:User"]};Password={config["SqlServer:Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";

builder.Services.AddDbContext<AraintelsqlContext>(
    options => options.UseSqlServer(config["ConnectionStrings:AraintelsqlDBContextConnection"]));
builder.Services.AddDbContext<AraintelsoftDBContext>(
    options => options.UseSqlServer(config["ConnectionStrings:AraintelsoftDBContextConnection"]));
builder.Services.AddDbContext<AragonDorksContext>(
    options => options.UseSqlServer(config["ConnectionStrings:AragonDorksContextConnectionString"]));

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

builder.Services.AddSingleton<InterfazEmailSender>(provider => new EmailSender(config));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddAuthentication(options =>

{

    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;

    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;

    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

});




// Servicios de Buscador
builder.Services.AddScoped<IBuscadorLinkedinService, BuscadorService>();
builder.Services.AddScoped<IBuscadorAragondorks, BuscadorAragondorks>();

// Razor Pages
builder.Services.AddRazorPages();

// Controllers with Views
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
});

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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "search",
    pattern: "Agenda/Search",
    defaults: new { controller = "Agenda", action = "Search" });
app.MapControllerRoute(
    name: "searchdorks",
    pattern: "AragonDorks/searchDorks",
    defaults: new { controller = "AragonDorks", action = "SearchDorks" });

app.MapRazorPages();

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AragonDorksContext>();
    context.Database.Migrate();
}


app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Dashboard", action = "Index" });

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