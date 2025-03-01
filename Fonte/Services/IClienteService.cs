using Fonte.Models;

namespace Fonte.Services
{
    public interface IClienteService
    {
        Task<ResultModel> CadastrarNovoCliente(CadastrarClienteModel model);
    }
}
