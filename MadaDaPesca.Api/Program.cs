using MadaDaPesca.Api.Configurations;
using MadaDaPesca.Application.DependencyInject;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.DependencyInject;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers()
    .AddSwaggerConfiguration()
    .AddOpenApi();

var configurarHangFire = builder.Configuration["HangFire"]?.ToUpper() == "TRUE";

builder.Services
    .InjectServices(configurarHangFire)
    .InjectRepositories()
    .InjectHttpClient(builder.Configuration)
    .InjectJwt(builder.Configuration["Jwt:Key"]!, builder.Configuration["Jwt:Issue"]!, builder.Configuration["Jwt:Audience"]!)
    .InjectDbContext(builder.Configuration["ConnectionStrings:Conexao"]!)
    .AddCorsConfiguration(builder.Configuration["Origins"]!.Split("|"));

if (configurarHangFire)
{
    builder.Services.ConfigurarHangFire(builder.Configuration["ConnectionStrings:Hangfire"]!);
}

LogService.ConfigureLog(builder.Configuration["Seq:Url"]!);
builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseMiddlewareConfiguration();

app.UseAuthorization();

app.UseCors("base");

app.MapControllers();

using var scope = app.Services.CreateScope();
var migrationServico = scope.ServiceProvider.GetService<IMigrationRepository>();
if (migrationServico != null)
{
    await migrationServico.RodarMigrationAsync();
}

if (configurarHangFire)
{
    app.ConfigurarDashBoardHangFire();
}

app.Run();
