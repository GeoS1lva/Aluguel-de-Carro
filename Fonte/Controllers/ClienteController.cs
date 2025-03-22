using Fonte.Models;
using Fonte.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fonte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(IClienteService clienteService) : ControllerBase
    {
        [HttpPost]
        [Route("novo")]
        public async Task<IActionResult> CadastrarCliente([FromBody] CadastrarClienteModel request)
        {
            var retorno = await clienteService.CadastrarNovoCliente(request);

            if (retorno.Error)
                return BadRequest(retorno.ErrorMessage);

            return Ok(retorno);
        }
    }
}
