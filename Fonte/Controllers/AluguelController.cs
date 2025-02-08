using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AluguelController(IAluguelService aluguelService) : ControllerBase
{
    [HttpPost]
    [Route("solicitar")]
    public async Task<IActionResult> SolicitarAlguel([FromBody] SolicitacaoAluguelCarroModel request)
    {
        var retorno = await aluguelService.AlugarCarroCliente(request);

        if (retorno.Error)
            return BadRequest(retorno.ErrorMessage);

        return Ok(retorno);
    }
}
