using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly ILoginGuiaDePescaService _loginGuiaDePescaService;
    public LoginController(ILoginGuiaDePescaService loginGuiaDePescaService)
    {
        _loginGuiaDePescaService = loginGuiaDePescaService;
    }

    [HttpPost("guia-de-pesca")]
    [ProducesResponseType<LoginGuiaDePescaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
    {
        var result = await _loginGuiaDePescaService.LoginAsync(loginDTO);
        return Ok(result);
    }
}
