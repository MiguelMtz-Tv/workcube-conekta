global using workcube_pagos.Models;
global using workcube_pagos.Data;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workcube.JwtAutentication;
using workcube_pagos.Services;
using workcube_pagos.TokenHandler;
using System.Globalization;
using Quartz;
using workcube_pagos.Jobs;

var builder = WebApplication.CreateBuilder(args);
//Inyecta las dependencias de JWT.
builder.Services.AddCustomJwtAuthentication();

//connection
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrar servicios
builder.Services.AddScoped<AspNetUsersService, AspNetUsersService>();
builder.Services.AddScoped<ServiciosService,  ServiciosService>();

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

//Configuracion de Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("VerifyServiceExpiration");
    q.AddJob<VerifyServicesExpiration>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("VerifyServiceExpiration-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule("0 * * ? * *")
    );
});
//builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

var cultureInfo = new CultureInfo("es-MX");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
