using Fonte.Repositories;
using Fonte.Entities;
using Fonte.Models;
using System.Net.Mail;

namespace Fonte.Services
{
    public class ClienteService(IUnitOfWork context) : IClienteService
    {
        private const string
            CPF_INVALIDO = "Cpf Inválido.",
            EMAIL_INVALIDO = "Estrutura do Email Inválido",
            NOME_COMPLETO_INVALIDO = "É necessário informar no mínimo nome e sobrenome.",
            CLIENTE_JA_CADASTRADO = "Esse Cliente ja esta cadastrado!",
            EMAIL_DO_CLIENTE_JA_CADASTRADO = "Email do Cliente ja esta cadastrado!";

        private string erro = string .Empty;

        public async Task<ResultModel> CadastrarNovoCliente(CadastrarClienteModel model)
        {
            if (!ValidarCpf(model.Cpf))
                return new(CPF_INVALIDO);

            if (!ValidarEmail(model.Email) || !ValidarNome(model.Nome))
                return new(erro);

            if (!await VerificarCpfJaCadastradoCliente(model.Cpf))
                return new(CLIENTE_JA_CADASTRADO);

            if (!await ValidarEmailClienteExistente(model.Email))
                return new(EMAIL_DO_CLIENTE_JA_CADASTRADO);

            var NovoCliente = new Cliente(model.Nome, model.Cpf, model.Email, model.Cep);

            context.ClienteRepository.InserirCliente(NovoCliente);

            await context.SaveChangesAsync();

            ClienteModel NovoClienteModel = new ClienteModel(NovoCliente.Nome, NovoCliente.Cpf, NovoCliente.Email, NovoCliente.Cep);

            return new(NovoClienteModel);

        }

        private async Task<bool> VerificarCpfJaCadastradoCliente(string cpf)
        {
            var cliente = await context.ClienteRepository.BuscarClientePorCpf(cpf);

            return cliente is null;
        }

        private async Task<bool> ValidarEmailClienteExistente(string email)
        {
            var cliente = await context.ClienteRepository.BuscarClientePorEmail(email);

            return cliente is null;
        }

        private static bool ValidarCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int primeiroDigitoVerificador = 11 - (soma % 11);
            if (primeiroDigitoVerificador >= 10)
                primeiroDigitoVerificador = 0;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            int segundoDigitoVerificador = 11 - (soma % 11);
            if (segundoDigitoVerificador >= 10)
                segundoDigitoVerificador = 0;

            return cpf[9] == primeiroDigitoVerificador.ToString()[0] && cpf[10] == segundoDigitoVerificador.ToString()[0];
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
    }
}
