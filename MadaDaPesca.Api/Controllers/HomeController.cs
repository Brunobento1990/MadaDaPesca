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

    public HomeController(IHomeGuiaDePescaService homeGuiaDePescaService)
    {
        _homeGuiaDePescaService = homeGuiaDePescaService;
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
}
