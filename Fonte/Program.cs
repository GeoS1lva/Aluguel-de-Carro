using Microsoft.EntityFrameworkCore;
using Test.Context;
using Test.Repositories;
using Test.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<SqlServerDbContext>();
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSql")));

builder.Services.AddScoped<ICarroRepository, CarroRepository>();
builder.Services.AddScoped<IAluguelRepository, AluguelRepository>();

builder.Services.AddScoped<IAluguelService, AluguelService>();
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
var carroRepository = scope.ServiceProvider.GetRequiredService<ICarroRepository>();

await carroRepository.InicializarDadosAsync();

app.Run();
