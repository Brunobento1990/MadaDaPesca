using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("guia-de-pesca")]
public class GuiaDePescaController : ControllerBase
{
    private readonly IGuiaDePescaService _guiaDePescaService;
    public GuiaDePescaController(IGuiaDePescaService guiaDePescaService)
    {
        _guiaDePescaService = guiaDePescaService;
    }

    [HttpPost("cadastrar")]
    [ProducesResponseType<GuiaDePescaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> CreateGuia([FromBody] GuiaDePescaCreateDTO guiaDePescaCreateDTO)
    {
        var guia = await _guiaDePescaService.CreateAsync(guiaDePescaCreateDTO);
        return Created($"/guia-de-pesca/obter-por-id?id={guia.Id}", guia);
    }

    [HttpGet("minha-conta")]
    [Autentica]
    [ProducesResponseType<GuiaDePescaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> MinhaConta()
    {
        var guia = await _guiaDePescaService.MinhaContaAsync();
        return Ok(guia);
    }

    [HttpPut("editar-minha-conta")]
    [Autentica]
    [ProducesResponseType<GuiaDePescaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> EditarMinhaConta(GuiaDePescaEditarDTO guiaDePescaEditar)
    {
        var guia = await _guiaDePescaService.EditarMinhaContaAsync(guiaDePescaEditar);
        return Ok(guia);
    }

    [HttpGet("obter-perfil")]
    [ProducesResponseType<GuiaDePescaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> ObterPerfil([FromQuery] Guid id)
    {
        var guia = await _guiaDePescaService.ObterPerfilAsyncAsync(id);
        return Ok(guia);
    }
}
