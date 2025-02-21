using Fonte.Entities;

namespace Fonte.Repositories;

public interface IAluguelRepository
{
    void InserirAluguel(Aluguel aluguel);
    Task<Aluguel?> ObterAluguelIdCliente(int id);
}