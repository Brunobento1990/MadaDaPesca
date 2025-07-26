using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("confirmar-conta-guia-de-pesca")]
public class ConfirmarContaGuiaDePescaController : ControllerBase
{
    private readonly IConfirmarContaGuiaDePescaService _confirmarContaGuiaDePescaService;
    public ConfirmarContaGuiaDePescaController(IConfirmarContaGuiaDePescaService confirmarContaGuiaDePescaService)
    {
        _confirmarContaGuiaDePescaService = confirmarContaGuiaDePescaService;
    }

    [HttpPut]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> ConfirmarContaAsync([FromQuery] Guid token)
    {
        await _confirmarContaGuiaDePescaService.ConfirmarContaAsync(token);
        return Ok("Conta confirmada com sucesso.");
    }
}
