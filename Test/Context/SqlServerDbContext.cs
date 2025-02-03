using Microsoft.EntityFrameworkCore;
using Test.Entities;

namespace Test.Context
{
    public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
    {
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
    }
}
