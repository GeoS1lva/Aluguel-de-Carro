using Fonte.Entities;
using Fonte.Repositories;

namespace Fonte.Services
{
    public class 
        TarefaAtualizandoTaxaService(IUnitOfWork context) : ITarefaAtualizandoTaxaService
    {
        public async Task VerificarTaxadeAtraso()
        {
            var alugueisVencidos = await context.AluguelRepository.VerificarAlugueisVencidos();

            foreach (Aluguel l in alugueisVencidos)
            {
                double taxa = ValidarTaxaPorAtraso(l.DataDevolucao, l.ValorTotal);

                l.TaxaAtrasoDevolucao = taxa;
                l.atrasado();

                context.SaveChangesAsync();
            }
        }

        public static double ValidarTaxaPorAtraso(DateOnly dataDevolucaoAluguel, double valorAluguel)
        {
            DateOnly dataDevolucaoDefinitiva = DateOnly.FromDateTime(DateTime.Now);

            var diferenca = dataDevolucaoDefinitiva.DayNumber - dataDevolucaoAluguel.DayNumber;

            if (diferenca == 0)
                return 0.0;

            return diferenca * (valorAluguel * 0.05);
        }
    }
}
