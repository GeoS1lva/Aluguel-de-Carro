using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Fonte.Models;
using Fonte.Repositories;
using Fonte.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit.Abstractions;

namespace Validações.Testes
{
    public class AluguelServiceTest
    {
        private readonly ITestOutputHelper _output;
        private Faker _faker = new("pt_BR");
        private Mock<IUnitOfWork> MockUnitOfWorl;
        private IAluguelService _service;

        public AluguelServiceTest(ITestOutputHelper test)
        {
            _output = test;
            MockUnitOfWorl = new Mock<IUnitOfWork>();
            _service = new AluguelService(MockUnitOfWorl.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Mensagem_Erro_Se_Cpf_Invalido()
        {

            string cpfInvalido = "12312345678";

            var model = new SolicitacaoAluguelCarroModel { 
                CpfCliente = cpfInvalido, 
                NomeCliente = "Teste testavel", 
                EmailCliente = "Teste@teste.com", 
                CepCliente = "12345678"
            };

            var actual = await _service.AlugarCarroCliente(model);

            Assert.True(actual.Error);
            _output.WriteLine($"{actual.ErrorMessage}");
        }

        [Fact]
        public async Task Deve_Retornar_Mensagem_Erro_Se_Email_Invalido()
        {
            string emailInvalido = "teste_gmail.com";

            var model = new SolicitacaoAluguelCarroModel
            {
                CpfCliente = _faker.Person.Cpf(),
                NomeCliente = "Teste testavel",
                EmailCliente = emailInvalido,
                CepCliente = "12345678"
            };

            var actual = await _service.AlugarCarroCliente(model);

            //Assert.True(resultado.Error);

            actual.Error.Should().BeTrue();
            _output.WriteLine(actual.ErrorMessage);
        }

        [Fact]
        public async Task Deve_Retornar_Mensagem_Erro_Ao_Inserir_DataInicial_DataFinal_Invalido()
        {
            DateOnly dataInicial = new DateOnly(2025, 02, 15);
            DateOnly dataFinal = new DateOnly(2025, 02, 10);

            var model = new SolicitacaoAluguelCarroModel
            {
                CpfCliente = _faker.Person.Cpf(),
                NomeCliente = "Teste testavel",
                EmailCliente = "Teste@teste.com",
                CepCliente = "12345678",
                DataRetirada = dataInicial,
                DataDevolucao = dataFinal
            };

            var actual = await _service.AlugarCarroCliente(model);

            actual.Error.Should().BeTrue();
            _output.WriteLine(actual.ErrorMessage);

        }
    }
}
