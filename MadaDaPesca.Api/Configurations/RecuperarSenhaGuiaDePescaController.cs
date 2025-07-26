using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Configurations;

[ApiController]
[Route("recuperar-senha-guia-de-pesca")]
public class RecuperarSenhaGuiaDePescaController : ControllerBase
{
    private readonly IRecuperarSenhaGuiaDePescaService _recuperarSenhaGuiaDePescaService;
    public RecuperarSenhaGuiaDePescaController(IRecuperarSenhaGuiaDePescaService recuperarSenhaGuiaDePescaService)
    {
        _recuperarSenhaGuiaDePescaService = recuperarSenhaGuiaDePescaService;
    }

    [HttpPut]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> RecuperarSenha([FromBody] RecuperarSenhaDTO recuperarSenhaDTO)
    {
        await _recuperarSenhaGuiaDePescaService.RecuperarSenhaAsync(recuperarSenhaDTO);
        return Ok("Senha recuperada com sucesso.");
    }
}
