using Microsoft.EntityFrameworkCore;
using Fonte.Context;
using Fonte.Entities;
using Fonte.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace Fonte.Repositories;

public class AluguelRepository(SqlServerDbContext context) : IAluguelRepository
{
    private readonly SqlServerDbContext _context = context;

    public void InserirAluguel(Aluguel aluguel)
    {
        _context.Alugueis.Add(aluguel);
    }
    public async Task<Aluguel?> ObterAluguelIdCliente(int id)
        => await _context.Alugueis.FirstOrDefaultAsync(x => x.ClienteId == id);
}
