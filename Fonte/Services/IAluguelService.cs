using Fonte.Models;

namespace Fonte.Services
{
    public interface IAluguelService
    {
        Task<ResultModel> AlugarCarroCliente(SolicitacaoAluguelCarroModel model);
    }
}