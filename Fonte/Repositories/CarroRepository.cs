using Microsoft.EntityFrameworkCore;
using Fonte.Context;
using Fonte.Entities;
using Fonte.Enums;
using System.Linq;

namespace Fonte.Repositories;

public class CarroRepository(SqlServerDbContext context) : ICarroRepository
{
    private readonly SqlServerDbContext _context = context;

    public async Task<List<Carro>> ObterCarrosPorTipoAsync(TipoCarro tipo)
        => await _context.Carros.Where(x => x.Tipo == tipo && x.QuantidadeDisponivel > 0).OrderBy(x => x.ValorAluguelDia).ToListAsync();
    public void Atualizar(Carro carro)
        => _context.Carros.Update(carro);

    public async Task InicializarDadosAsync()
    {
        if (_context.Carros.Any()) return;

        List<Carro> listaCarros =

        [
            // TipoCarro: SUV
            new Carro("Compass", "Jeep", 2022, TipoCarro.SUV, 1, 250.00),
            new Carro("Creta", "Hyundai", 2023, TipoCarro.SUV, 1, 220.00),
            new Carro("Hilux", "Toyota", 2023, TipoCarro.SUV, 1, 350.00),

            // TipoCarro: Sedan
            new Carro("Virtus", "Volkswagen", 2021, TipoCarro.Sedan, 2, 180.00),
            new Carro("Corolla", "Toyota", 2022, TipoCarro.Sedan, 3, 250.00),
            new Carro("Civic", "Honda", 2023, TipoCarro.Sedan, 1, 230.00),

            // TipoCarro: Compacto
            new Carro("Onix", "Chevrolet", 2023, TipoCarro.Compacto, 1, 120.00),
            new Carro("HB20", "Hyundai", 2022, TipoCarro.Compacto, 1, 110.00),
            new Carro("Mobi", "Fiat", 2023, TipoCarro.Compacto, 2, 100.00),

            // TipoCarro: Luxo
            new Carro("Classe C", "Mercedes-Benz", 2023, TipoCarro.Luxo, 2, 500.00),
            new Carro("A8", "Audi", 2023, TipoCarro.Luxo, 1, 600.00),
            new Carro("X5", "BMW", 2023, TipoCarro.Luxo, 1, 550.00),

            // TipoCarro: Hatch
            new Carro("Argo", "Fiat", 2023, TipoCarro.Hatch, 3, 130.00),
            new Carro("Gol", "Volkswagen", 2022, TipoCarro.Hatch, 1, 110.00),
            new Carro("Onix Hatch", "Chevrolet", 2023, TipoCarro.Hatch, 2, 120.00),

            // TipoCarro: MiniVan
            new Carro("Spin", "Chevrolet", 2020, TipoCarro.MiniVan, 2, 160.00),
            new Carro("Journey", "Dodge", 2021, TipoCarro.MiniVan, 2, 180.00),
            new Carro("Carnival", "Kia", 2023, TipoCarro.MiniVan, 2, 220.00)
        ];

        await _context.Carros.AddRangeAsync(listaCarros);
        await _context.SaveChangesAsync();
    }
}
