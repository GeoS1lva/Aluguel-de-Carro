
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
            CPF_INVALIDO = "Cpf Inválido.",
            EMAIL_INVALIDO = "Email Inválido.",
            NOME_COMPLETO_INVALIDO = "É necessário informar no mínimo nome e sobrenome.",
            CARRO_NAO_DISPONIVEL = "Não temos carros disponíveis",
            DOMINGO_UM_DIA_INVALIDO = "Não é possível alugar somente ao domingo e/ou somente 1 dia",
            DATA_INVALIDA = "Data Invalida",
            CLIENTE_JA_POSSUI_CARRO_ALUGADO = "Operação Inválida! Cliente já possui um carro alugado";

        private string erro = string.Empty;

        public async Task<ResultModel> AlugarCarroCliente(SolicitacaoAluguelCarroModel model)
        {
            if (!ValidarCpf(model.CpfCliente))
                return new(CPF_INVALIDO);

            if (!ValidarEmail(model.EmailCliente)
                || !ValidarNome(model.NomeCliente))
                return new(erro);
               
            if (!ValidarData(model.DataRetirada, model.DataDevolucao))
                return new(erro);

            var carros = await context.CarroRepository.ObterCarrosPorTipoAsync(model.TipoCarro);
            var carro = carros.FirstOrDefault();

            if (carro is null || carro.QuantidadeDisponivel <= 0)
                return new(CARRO_NAO_DISPONIVEL);

            carro.AlugarUm();

            context.CarroRepository.Atualizar(carro);

            var quantidadeDias = CalcularQuantidadeDias(model.DataRetirada, model.DataDevolucao);

            var valorTotal = carro.ValorAluguelDia * quantidadeDias;

            var cliente = await VerificarECadastrarNovoCliente(model);

            if (await ValidarUmCpfPorCarro(cliente.ClienteId) == false)
                return new(CLIENTE_JA_POSSUI_CARRO_ALUGADO);

            var aluguel = new Aluguel(quantidadeDias, valorTotal, carro.Modelo, carro.Marca, carro.Ano, model.TipoCarro, cliente.ClienteId);

            context.AluguelRepository.InserirAluguel(aluguel);

            await context.SaveChangesAsync();

            var aluguelModel = new AluguelModel(model.CpfCliente, model.NomeCliente, model.EmailCliente, model.CepCliente, quantidadeDias, valorTotal, carro.Modelo, carro.Marca, carro.Ano, model.TipoCarro);

            return new(aluguelModel);
        }

        private async Task<Cliente> VerificarECadastrarNovoCliente(SolicitacaoAluguelCarroModel model)
        {
            var clientes = await context.ClienteRepository.BuscarClientePorCpf(model.CpfCliente);

            if (clientes is null)
            {
                var novoCliente = new Cliente(model.NomeCliente, model.CpfCliente, model.EmailCliente, model.CepCliente);
                context.ClienteRepository.InserirCliente(novoCliente);
                await context.SaveChangesAsync();
                return novoCliente;
            }

            var cliente = clientes;
            return cliente;
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

        private bool ValidarEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
            }
            catch (Exception)
            {
                erro = EMAIL_INVALIDO;
                return false;
            }

            return true;
        }

        private bool ValidarNome(string nome)
        {
            if (nome.IndexOf(" ") == 0 || !nome.Contains(" "))
            {
                erro = NOME_COMPLETO_INVALIDO;
                return false;
            }

            return true;
        }

        private static bool ValidarCpf(string cpf)
        {
            // Remove qualquer caractere não numérico
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem 11 caracteres
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os números são iguais (casos como 111.111.111-11)
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Valida o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int primeiroDigitoVerificador = 11 - (soma % 11);
            if (primeiroDigitoVerificador >= 10)
                primeiroDigitoVerificador = 0;

            // Valida o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            int segundoDigitoVerificador = 11 - (soma % 11);
            if (segundoDigitoVerificador >= 10)
                segundoDigitoVerificador = 0;

            // Verifica se os dígitos calculados são iguais aos fornecidos
            return cpf[9] == primeiroDigitoVerificador.ToString()[0] && cpf[10] == segundoDigitoVerificador.ToString()[0];
        }

        private async Task<bool> ValidarUmCpfPorCarro(int Id)
        {
            var aluguel = await context.AluguelRepository.ObterAluguelIdCliente(Id);

            return aluguel is null;
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
