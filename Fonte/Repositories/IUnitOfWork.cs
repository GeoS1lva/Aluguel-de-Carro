using Fonte.Repositories;

namespace Fonte.Repositories
{
    public interface IUnitOfWork
    {
        IAluguelRepository AluguelRepository { get; }
        ICarroRepository CarroRepository { get; }
        IClienteRepository ClienteRepository { get; }
        Task SaveChangesAsync();
    }
}
