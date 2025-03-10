using Fonte.Models;
using Fonte.Repositories;
using Fonte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucaoController(IDevolverAluguelService devolverAluguel) : ControllerBase
    {
        [HttpPost]
        [Route("Carro")]
        public async Task<IActionResult> DevolverCarro([FromBody] DevolverAluguelModel request) 
        {
            var retorno = await devolverAluguel.RealizarDevolucaoDeAluguel(request);

            if(retorno.Error)
                return BadRequest(retorno.ErrorMessage);

            return Ok(retorno);
        }
    }
}
