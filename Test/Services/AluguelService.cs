
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using Test.Context;
using Test.Entities;
using Test.Enums;
using Test.Models;
using Test.Repositories;

namespace Test.Services
{
    public class AluguelService(IUnitOfWork context) : IAluguelService
    {
        private const string
            CPF_INVALIDO = "Cpf Inválido.",
            EMAIL_INVALIDO = "Email Inválido.",
            NOME_COMPLETO_INVALIDO = "É necessário informar no mínimo nome e sobrenome.",
            CARRO_NAO_DISPONIVEL = "Não temos carros disponíveis",
            DOMINGO_INVALIDO = "Não é possível alugar somente ao domingo e/ou somente 1 dia",
            DATA_INVALIDA = "Data Invalida";

        private string erro = string.Empty;

        public async Task<ResultModel> AlugarCarroCliente(SolicitacaoAluguelCarroModel model)
        {
            if (!ValidarEmail(model.EmailCliente)
                || !ValidarNome(model.NomeCliente))
                return new(erro);

            if (!ValidarCpf(model.CpfCliente))
                return new(CPF_INVALIDO);
               
            if (!ValidarData(model.DataRetirada, model.DataDevolucao))
                return new(erro);

            var carros = await context.CarroRepository.ObterCarrosPorTipoAsync((TipoCarro)model.TipoCarro);
            var carro = carros.FirstOrDefault();

            if (carro is null || carro.QuantidadeDisponivel <= 0)
                return new(CARRO_NAO_DISPONIVEL);

            carro.AlugarUm();

            context.CarroRepository.Atualizar(carro);

            var quantidadeDias = CalcularQuantidadeDias(model.DataRetirada, model.DataDevolucao);

            if (quantidadeDias == 0)
                return new(DOMINGO_INVALIDO);

            var valorTotal = carro.ValorAluguelDia * quantidadeDias;

            var aluguel = new Aluguel(model.CpfCliente, model.NomeCliente, model.EmailCliente, model.CepCliente, quantidadeDias, valorTotal, carro.Modelo, carro.Marca, carro.Ano, (TipoCarro)model.TipoCarro);

            context.AluguelRepository.InserirAluguel(aluguel);

            await context.SaveChangesAsync();

            var aluguelModel = new AluguelModel(model.CpfCliente, model.NomeCliente, model.EmailCliente, model.CepCliente, quantidadeDias, valorTotal, carro.Modelo, carro.Marca, carro.Ano, (TipoCarro)model.TipoCarro);

            return new(aluguelModel);
        }

        private int CalcularQuantidadeDias(DateOnly dataInicial, DateOnly dataFinal)
        {
            var intervaloData = dataFinal.DayNumber - dataInicial.DayNumber;

            if (intervaloData == 1 && dataInicial.DayOfWeek == DayOfWeek.Sunday)
                return 0;

            var total = intervaloData;

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

        private bool ValidarData(DateOnly retirada, DateOnly devolucao)
        {
            if(retirada > devolucao)
            {
                erro = DATA_INVALIDA;
                return false;
            }

            return true;
        }
    }
}
