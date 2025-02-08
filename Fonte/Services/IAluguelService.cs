using Test.Models;

namespace Test.Services
{
    public interface IAluguelService
    {
        Task<ResultModel> AlugarCarroCliente(SolicitacaoAluguelCarroModel model);
    }
}