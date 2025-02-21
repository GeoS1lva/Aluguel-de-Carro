using Fonte.Context;
using Fonte.Entities;

namespace Fonte.Repositories
{
    public interface IClienteRepository
    {
        public void InserirCliente(Cliente cliente);
        public Task<Cliente?> BuscarClientePorCpf(string cpf);
    }
}
