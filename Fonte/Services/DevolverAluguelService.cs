using Fonte.Models;
using Fonte.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Fonte.Services
{
    public class DevolverAluguelService(IUnitOfWork context) : IDevolverAluguelService
    {

        private const string
            CLIENTE_NAO_CADASTRADO = "Cliente não Cadastrado!",
            CARRO_NAO_ENCONTRADO = "Placa de Carro não identificada!",
            ALUGUEL_NAO_ENCONTRADO = "Aluguel não encontrado!";

        private string erro = string.Empty;

        public async Task<ResultModel> RealizarDevolucaoDeAluguel(DevolverAluguelModel model)
        {
            var cliente = await context.ClienteRepository.BuscarClientePorCpf(model.Cpf);

            if (cliente is null)
                return new(CLIENTE_NAO_CADASTRADO);

            var carro = await context.CarroRepository.ObterCarroPelaPlaca(model.PlacaCarro);

            if (carro is null)
                return new(CARRO_NAO_ENCONTRADO);

            var aluguelCliente = await context.AluguelRepository.ObterAluguelPorIdCarro(carro.Id);

            if (aluguelCliente is null)
                return new(ALUGUEL_NAO_ENCONTRADO);

            carro.DevolverCarro();

            aluguelCliente.ValorPago = aluguelCliente.ValorTotal + aluguelCliente.TaxaAtrasoDevolucao;

            aluguelCliente.finalizado();

            await context.SaveChangesAsync();

            var resultadoDevolucao = new ResultDevolucaoModel(aluguelCliente.TaxaAtrasoDevolucao, aluguelCliente.ValorPago);

            return new(resultadoDevolucao);
        }
    }
}
