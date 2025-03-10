using Microsoft.EntityFrameworkCore;
using Fonte.Context;
using Fonte.Entities;
using Fonte.Repositories;
using System.Reflection.Metadata.Ecma335;
using Fonte.Enums;

namespace Fonte.Repositories;

public sealed class AluguelRepository(SqlServerDbContext context) : IAluguelRepository
{
    private readonly SqlServerDbContext _context = context;

    public void InserirAluguel(Aluguel aluguel)
    {
        _context.Alugueis.Add(aluguel);
    }
    public async Task<Aluguel?> ObterAluguelIdCliente(int id)
        => await _context.Alugueis.FirstOrDefaultAsync(x => x.ClienteId == id);

    public async Task<Aluguel?> ObterAluguelPorIdCarro(int id)
        => await _context.Alugueis.FirstOrDefaultAsync(x => x.CarroId == id);

    public async Task<List<Aluguel>> RetornarAlugueisVencidos()
    {
        DateOnly DataAtual = DateOnly.FromDateTime(DateTime.Now);

        return await _context.Alugueis.Where(x => x.DataDevolucao < DataAtual).ToListAsync();
    }
    public async Task<List<Aluguel>> VerificarAlugueisVencidos()
    {
        DateOnly DataAtual = DateOnly.FromDateTime(DateTime.Now);

        return await _context.Alugueis.Where(x => x.DataDevolucao < DataAtual && x.status == EstadoAlguel.valido)
            .ToListAsync();
    }
}
