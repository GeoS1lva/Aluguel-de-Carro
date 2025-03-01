using Fonte.Context;
using Fonte.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fonte.Repositories
{
    public sealed class ClienteRepository(SqlServerDbContext context) : IClienteRepository
    {
        private readonly SqlServerDbContext _context = context;

        public void InserirCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public async Task<Cliente?> BuscarClientePorCpf(string cpf)
            => await _context.Clientes.FirstOrDefaultAsync(x => x.Cpf == cpf);

        public async Task<Cliente?> BuscarClientePorEmail(string email)
            => await _context.Clientes.FirstOrDefaultAsync(x => x.Email == email);
    }
}
