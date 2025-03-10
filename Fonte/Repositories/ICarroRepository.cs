
using Fonte.Entities;
using Fonte.Enums;

namespace Fonte.Repositories;

public interface ICarroRepository
{
    Task InicializarDadosAsync();
    Task<List<Carro>> ObterCarrosPorTipoAsync(TipoCarro tipo);
    Task<Carro> ObterCarroPelaPlaca(string placa);
    void Atualizar(Carro carro);
}