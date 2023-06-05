global using workcube_pagos.Models;
global using workcube_pagos.Data;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Stripe;
using workcube_pagos.Services;
using workcube_pagos.TokenHandler;
using System.Globalization;
using Quartz;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//configuring stripe test key
StripeConfiguration.ApiKey = "sk_test_51NDAKSHo0a7qOJb8fswzrmT31QLoRDB6Hp8HWXNvEEg8JXpE3xnxGMWDrGOFiW5lo5AZ8Cjshh4RT7MJIpBatsUt006xpsXGTR";

//inject jwt authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Validar el emisor
        ValidateAudience = false, // Validar la audiencia
        ValidateLifetime = true, // Validar el tiempo de vida del token
        ValidateIssuerSigningKey = true, // Validar la firma

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP"))
    };
});

//connection
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrar servicios
builder.Services.AddScoped<AspNetUsersService, AspNetUsersService>();
builder.Services.AddScoped<ServiciosService,  ServiciosService>();
builder.Services.AddScoped<CuponesService, CuponesService>();
builder.Services.AddScoped<ClientesService, ClientesService>();
builder.Services.AddScoped<TarjetasService, TarjetasService>();
builder.Services.AddScoped<PagosService, PagosService>();

// Generador de contrase�a
builder.Services.AddIdentity<AspNetUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtTokenHandler>();
// Configurar las opciones de serialización globalmente
//builder.Services.AddMvc().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    options.JsonSerializerOptions.MaxDepth = 32;
//});

var app = builder.Build();

var cultureInfo = new CultureInfo("es-MX");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

//
var jwtSecurityKey = "TuClaveDeFirmaSecreta";
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}"
    
);

app.MapFallbackToFile("index.html");

app.Run();