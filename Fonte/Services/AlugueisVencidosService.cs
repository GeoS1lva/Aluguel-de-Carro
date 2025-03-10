using System.ComponentModel;
using System.Net.WebSockets;
using Fonte.Entities;
using Fonte.Models;
using Fonte.Repositories;

namespace Fonte.Services
{
    public class AlugueisVencidosService(IUnitOfWork context) : IAlugueisVencidosService
    {
        private const string
            SEM_ALUGUEIS_VENCIDOS = "Não foi encontrado Alugueis com atraso a data de Devolução!";

        

        public async Task<ResultModel> RetornaAlugueisVencidos()
        {
            var alugueisVencidos = await context.AluguelRepository.RetornarAlugueisVencidos();

            if (alugueisVencidos is null)
                return new(SEM_ALUGUEIS_VENCIDOS);

            var vencidos = alugueisVencidos.Select(l => new ListaAlugueisVencidosModel(l.Id, l.ClienteId, l.CarroId, l.DataDevolucao));

            return new(vencidos);
        }

        public async Task RetornarArquivoCSValugueisVencidos()
        {
            var alugueisVencidos = await context.AluguelRepository.RetornarAlugueisVencidos();

            string pastaDestino = @"C:\Users\Geovana\Documents\AlugueisEmAtrasoDeDevolucao.csv";

            using (StreamWriter sr = new StreamWriter(pastaDestino))
            {
                await sr.WriteLineAsync("IdAluguel,QuantidadeDias,ValorTotal,DataRetirada,DataDevolucao,CarroId,ClienteId");

                foreach (Aluguel vencidos in alugueisVencidos)
                {
                    await sr.WriteLineAsync($"{vencidos.Id},{vencidos.QuantidadeDias},{vencidos.ValorTotal},{vencidos.DataRetirada},{vencidos.DataDevolucao},{vencidos.CarroId},{vencidos.ClienteId}");
                }
            }
        }
    }
}
