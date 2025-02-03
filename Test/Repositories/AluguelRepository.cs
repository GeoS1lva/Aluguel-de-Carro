 using Microsoft.EntityFrameworkCore;
using Test.Context;
using Test.Entities;

namespace Test.Repositories;

public class AluguelRepository(SqlServerDbContext context) : IAluguelRepository
{
    private readonly SqlServerDbContext _context = context;

    public void InserirAluguel(Aluguel aluguel)
    {
        _context.Alugueis.Add(aluguel);
    }
}
