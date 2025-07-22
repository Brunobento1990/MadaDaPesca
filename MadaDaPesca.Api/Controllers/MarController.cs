using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("mar")]
public class MarController : ControllerBase
{
    private readonly IMarService _marService;
    public MarController(IMarService marService)
    {
        _marService = marService;
    }
    [HttpGet("informacoes")]
    [Autentica]
    [ProducesResponseType<InformacoesDoMarViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> ObterAlturaOndaAsync([FromQuery] double latitude, [FromQuery] double longitude)
    {
        var resultado = await _marService.ObterInformacoesAsync(latitude, longitude);
        return Ok(resultado);
    }
}
