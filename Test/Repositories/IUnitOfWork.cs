using Test.Repositories;

namespace Test.Repositories
{
    public interface IUnitOfWork
    {
        IAluguelRepository AluguelRepository { get; }
        ICarroRepository CarroRepository { get; }
        Task SaveChangesAsync();
    }
}
