using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Fonte.Context;
using Fonte.Entities;
using Fonte.Enums;
using Fonte.Models;
using Fonte.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fonte.Services
{
    public class AluguelService(IUnitOfWork context) : IAluguelService
    {
        private const string
            CARRO_NAO_DISPONIVEL = "Não temos carros disponíveis",
            DOMINGO_UM_DIA_INVALIDO = "Não é possível alugar somente ao domingo e/ou somente 1 dia",
            CLIENTE_NAO_CADASTRADO = "Cliente não Cadastrado!",
            DATA_INVALIDA = "Data Invalida";

        private string erro = string.Empty;

        public async Task<ResultModel> AlugarCarroCliente(SolicitacaoAluguelCarroModel model)
        {

            var cliente = await context.ClienteRepository.BuscarClientePorCpf(model.CpfCliente);

            if (cliente is null)
                return new(CLIENTE_NAO_CADASTRADO);

            if (!ValidarData(model.DataRetirada, model.DataDevolucao))
                return new(erro);

            var carros = await context.CarroRepository.ObterCarrosPorTipoAsync(model.TipoCarro);
            var carro = carros.FirstOrDefault();

            if (carro is null || carro.Disponivel == false)
                return new(CARRO_NAO_DISPONIVEL);

            carro.AlugarUm();

            context.CarroRepository.Atualizar(carro);

            var quantidadeDias = CalcularQuantidadeDias(model.DataRetirada, model.DataDevolucao);

            var valorTotal = carro.ValorAluguelDia * quantidadeDias;

            var aluguel = new Aluguel(quantidadeDias, valorTotal, model.DataRetirada, model.DataDevolucao, carro.Id, cliente.Id);

            context.AluguelRepository.InserirAluguel(aluguel);

            await context.SaveChangesAsync();

            var aluguelModel = new AluguelModel(cliente.Cpf, cliente.Nome, cliente.Email, cliente.Cep, quantidadeDias, valorTotal, carro.Modelo, carro.Marca, carro.Ano, model.TipoCarro);

            return new(aluguelModel);
        }

        private static int CalcularQuantidadeDias(DateOnly dataInicial, DateOnly dataFinal)
        {
            var intervaloData = dataFinal.DayNumber - dataInicial.DayNumber;

            for(DateOnly data = dataInicial; data < dataFinal; data = data.AddDays(1))
            {
                if (data.DayOfWeek == DayOfWeek.Sunday)
                    intervaloData--;
            }

            return intervaloData;
        }

        private bool ValidarData(DateOnly retirada, DateOnly devolucao)
        {
            if (retirada > devolucao)
            {
                erro = DATA_INVALIDA;
                return false;
            }  
           
            if (retirada == devolucao)
            {
                erro = DOMINGO_UM_DIA_INVALIDO;
                return false;
            }  

            return true;
        }
    }
}
