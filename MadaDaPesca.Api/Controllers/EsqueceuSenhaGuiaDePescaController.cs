using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("esqueceu-senha-guia-de-pesca")]
public class EsqueceuSenhaGuiaDePescaController : ControllerBase
{
    private readonly IEsqueceuSenhaGuiaDePescaService _esqueceuSenhaGuiaDePescaService;

    public EsqueceuSenhaGuiaDePescaController(IEsqueceuSenhaGuiaDePescaService esqueceuSenhaGuiaDePescaService)
    {
        _esqueceuSenhaGuiaDePescaService = esqueceuSenhaGuiaDePescaService;
    }

    [HttpPut]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> EsqueceuSenha(EsqueceSenhaDTO esqueceSenhaDTO)
    {
        await _esqueceuSenhaGuiaDePescaService.EsqueceuSenhaAsync(esqueceSenhaDTO);
        return Ok("Verifique seu e-mail e siga as instruções");
    }
}
