using Fonte.Entities;

namespace Fonte.Repositories;

public interface IAluguelRepository
{
    void InserirAluguel(Aluguel aluguel);
    Task<Aluguel?> ObterAluguelIdCliente(int id);
    Task<Aluguel?> ObterAluguelPorIdCarro(int id);
    Task<List<Aluguel>> RetornarAlugueisVencidos();
    Task<List<Aluguel>> VerificarAlugueisVencidos();
}