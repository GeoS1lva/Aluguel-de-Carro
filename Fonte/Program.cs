using Microsoft.EntityFrameworkCore;
using Fonte.Context;
using Fonte.Repositories;
using Fonte.Services;
using Microsoft.IdentityModel.Tokens;
using Fonte.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SqlServerDbContext>();
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSql")));

builder.Services.AddScoped<IAluguelService, AluguelService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IDevolverAluguelService, DevolverAluguelService>();
builder.Services.AddScoped<IAlugueisVencidosService, AlugueisVencidosService>();
builder.Services.AddScoped<ITarefaAutomaticaService, TarefaAutomaticaService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<SqlServerDbContext>();

if (!context.Carros.Any())
{
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    await unitOfWork.CarroRepository.InicializarDadosAsync();
}

app.Run();
