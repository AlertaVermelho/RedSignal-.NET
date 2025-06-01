using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RedSignal.Data;
using RedSignal.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexão com banco Oracle
var connectionString = "User Id=xxxxxx;Password=xxxxxx;Data Source=localhost:1521/br.com.fiap.oracle;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(connectionString)
);

// Registro da camada de serviço
builder.Services.AddScoped<IMonitoredLocationService, MonitoredLocationService>();

// Autenticação com Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Admin/AccessDenied";
        options.LogoutPath = "/Admin/Logout";
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RedSignal API", Version = "v1" });
});

// Razor Pages (Admin)
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RedSignal API v1");
        c.RoutePrefix = "swagger"; // acessa via /swagger
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();    // REST API
app.MapRazorPages();     // Admin interface

app.Run();
