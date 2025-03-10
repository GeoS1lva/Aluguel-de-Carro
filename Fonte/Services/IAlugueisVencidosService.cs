using Fonte.Models;

namespace Fonte.Services
{
    public interface IAlugueisVencidosService
    {
        Task<ResultModel> RetornaAlugueisVencidos();
        Task RetornarArquivoCSValugueisVencidos();
    }
}
