
using Test.Entities;
using Test.Enums;

namespace Test.Repositories;

public interface ICarroRepository
{
    Task InicializarDadosAsync();
    Task<List<Carro>> ObterCarrosPorTipoAsync(TipoCarro tipo);
    void Atualizar(Carro carro);
}