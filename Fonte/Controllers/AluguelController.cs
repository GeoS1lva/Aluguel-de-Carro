using Microsoft.AspNetCore.Mvc;
using Fonte.Models;
using Fonte.Services;

namespace Fonte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AluguelController(IAluguelService aluguelService) : ControllerBase
{
    [HttpPost]
    [Route("solicitar")]
    public async Task<IActionResult> SolicitarAluguel([FromBody] SolicitacaoAluguelCarroModel request)
    {
        var retorno = await aluguelService.AlugarCarroCliente(request);

        if (retorno.Error)
            return BadRequest(retorno.ErrorMessage);

        return Ok(retorno);
    }
}
