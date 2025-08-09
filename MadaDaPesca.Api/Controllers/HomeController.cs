using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    private readonly IHomeGuiaDePescaService _homeGuiaDePescaService;
    private readonly IHomePescadorService _homePescadorService;

    public HomeController(IHomeGuiaDePescaService homeGuiaDePescaService, IHomePescadorService homePescadorService)
    {
        _homeGuiaDePescaService = homeGuiaDePescaService;
        _homePescadorService = homePescadorService;
    }

    [HttpGet("guia-de-pesca")]
    [Autentica]
    [ProducesResponseType<HomeViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> HomeGuiaDePesca([FromQuery] HomeDTO homeDTO)
    {
        var homeViewModel = await _homeGuiaDePescaService.ObterAsync(homeDTO);
        return Ok(homeViewModel);
    }

    [HttpGet("pescador")]
    [ProducesResponseType<HomePescadorViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Pescador()
    {
        var home = await _homePescadorService.HomeAsync();
        return Ok(home);
    }
}
