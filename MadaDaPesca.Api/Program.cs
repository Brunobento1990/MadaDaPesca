using MadaDaPesca.Api.Configurations;
using MadaDaPesca.Application.DependencyInject;
using MadaDaPesca.Infra.DependencyInject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers()
    .AddSwaggerConfiguration()
    .AddOpenApi();

builder.Services
    .InjectServices()
    .InjectRepositories()
    .InjectDbContext(builder.Configuration["ConnectionStrings:Conexao"]!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
