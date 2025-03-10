using Fonte.Models;
using Fonte.Repositories;

namespace Fonte.Services
{
    public interface IDevolverAluguelService
    {
        Task<ResultModel> RealizarDevolucaoDeAluguel(DevolverAluguelModel model);
    }
}
