using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("galeria-de-trofeu")]
public class GaleriaDeTrofeuController : ControllerBase
{
    private readonly IGaleriaDeTrofeuService _galeriaDeTrofeuService;

    public GaleriaDeTrofeuController(IGaleriaDeTrofeuService galeriaDeTrofeuService)
    {
        _galeriaDeTrofeuService = galeriaDeTrofeuService;
    }

    [HttpGet("minha-galeria")]
    [Autentica]
    [ProducesResponseType<IList<GaleriaDeTrofeuViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> MinhaGaleria()
    {
        var galeria = await _galeriaDeTrofeuService.GaleriaDoGuiaDePescaAsync();
        return Ok(galeria);
    }

    [HttpPost("adicionar")]
    [Autentica]
    [ProducesResponseType<IList<GaleriaDeTrofeuViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Adicionar([FromBody] IList<GaleriaDeTrofeuDTO> galeria)
    {
        var resultado = await _galeriaDeTrofeuService.AdicionarAsync(galeria);
        return Ok(resultado);
    }

    [HttpDelete("excluir")]
    [Autentica]
    [ProducesResponseType(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Excluir([FromQuery] Guid id)
    {
        await _galeriaDeTrofeuService.ExcluirAsync(id);
        return Ok(new { mensagem = "Registro escluido com sucesso" });
    }
}
