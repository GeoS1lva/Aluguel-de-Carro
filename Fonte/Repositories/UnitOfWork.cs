﻿using Fonte.Context;

namespace Fonte.Repositories
{
    public class UnitOfWork(SqlServerDbContext context) : IUnitOfWork
    {
        private readonly SqlServerDbContext _context = context;
        public IAluguelRepository AluguelRepository { get; } = new AluguelRepository(context);
        public ICarroRepository CarroRepository { get; } = new CarroRepository(context);
        public IClienteRepository ClienteRepository { get; } = new ClienteRepository(context);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
