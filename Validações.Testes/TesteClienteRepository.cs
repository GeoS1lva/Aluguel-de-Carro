using Fonte.Context;
using Fonte.Entities;
using Fonte.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Microsoft.Extensions.Primitives;

namespace Validações.Testes
{
    public class TesteClienteRepository
    {
        private readonly ITestOutputHelper _output;

        public TesteClienteRepository(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public async Task TesteBuscarCpfClienteNoBancoAsync()
        {
            //Arrage

            var cliente = new Cliente("Geovana", "13518477919", "Geovana@gmail.com", "87103232");

            var mockIClienteRepository = new Mock<IClienteRepository>();
            mockIClienteRepository.Setup(c => c.BuscarClientePorCpf("13518477919")).ReturnsAsync(cliente);

            var mockIUnitOfWork = new Mock<IUnitOfWork>();
            mockIUnitOfWork.Setup(c => c.ClienteRepository).Returns(mockIClienteRepository.Object);

            var unitOfWork = mockIUnitOfWork.Object;

            //Act

            var resultado = await unitOfWork.ClienteRepository.BuscarClientePorCpf("13518477919");

            //Assert

            Assert.NotNull(resultado);
            _output.WriteLine($"{resultado.NomeCliente}");
        }
    }
}
