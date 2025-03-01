using Microsoft.EntityFrameworkCore;
using Fonte.Entities;

namespace Fonte.Context
{
    public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
    {
        public DbSet<Carro>? Carros { get; set; }
        public DbSet<Aluguel>? Alugueis { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluguel>()
                .HasOne(x => x.Cliente)
                .WithMany(l => l.Alugueis)
                .HasForeignKey(e => e.ClienteId);

            modelBuilder.Entity<Aluguel>()
                .HasOne(x => x.Carro)
                .WithMany(l => l.Alugueis)
                .HasForeignKey(e => e.CarroId);
        }
    }
}
