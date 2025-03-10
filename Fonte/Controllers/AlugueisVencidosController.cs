using Fonte.Models;
using Fonte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisVencidosController(IAlugueisVencidosService alugueisVencidos) : ControllerBase
    {
        [HttpGet]
        [Route("retorno")]
        public async Task<ActionResult> ObterAlugueisVencidos()
        {
            var retorno = await alugueisVencidos.RetornaAlugueisVencidos();

            if(retorno.Error)
                return BadRequest(retorno.Error);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("ArquivoCSV")]
        public async Task<IActionResult> GerarArquivoCSValugueisVencidos()
        {
            await alugueisVencidos.RetornarArquivoCSValugueisVencidos();

            return Ok("Arquivo salvo com sucesso!");
        }
    }
}
