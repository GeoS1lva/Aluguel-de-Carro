using Microsoft.EntityFrameworkCore;
using Fonte.Entities;

namespace Fonte.Context
{
    public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
    {
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
