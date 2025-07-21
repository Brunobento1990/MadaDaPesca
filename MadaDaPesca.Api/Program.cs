using MadaDaPesca.Api.Configurations;
using MadaDaPesca.Application.DependencyInject;
using MadaDaPesca.Application.Services;
using MadaDaPesca.Infra.DependencyInject;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers()
    .AddSwaggerConfiguration()
    .AddOpenApi();

builder.Services
    .InjectServices()
    .InjectRepositories()
    .InjectJwt(builder.Configuration["Jwt:Key"]!, builder.Configuration["Jwt:Issue"]!, builder.Configuration["Jwt:Audience"]!)
    .InjectDbContext(builder.Configuration["ConnectionStrings:Conexao"]!);

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

app.MapControllers();

app.Run();
