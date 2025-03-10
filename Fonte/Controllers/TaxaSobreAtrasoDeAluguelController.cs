using Fonte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxaSobreAtrasoDeAluguelController(ITarefaAtualizandoTaxaService tarefaTaxa) : ControllerBase
    {
        [HttpGet]
        [Route("tarefaTaxaAtraso")]
        public async Task<IActionResult> AtualizarTaxaPorAtraso()
        {
            await tarefaTaxa.VerificarTaxadeAtraso();

            return Ok("Taxa Atualizada!");
        }
    }
}
