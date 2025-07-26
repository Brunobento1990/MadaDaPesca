using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("google")]
public class GoogleController : ControllerBase
{
    private readonly IGoogleAuthHttpClient _googleAuthHttpClient;

    public GoogleController(IGoogleAuthHttpClient googleAuthHttpClient)
    {
        _googleAuthHttpClient = googleAuthHttpClient;
    }

    [HttpPost("validar-token")]
    [ProducesResponseType<TokenResponseGoogleViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> ValidarTokenAsync([FromBody] LoginComGoogleDTO loginComGoogleDTO)
    {
        var responseGoogle = await _googleAuthHttpClient.ValidarTokenGoogleAsync(loginComGoogleDTO.Token);
        return Ok(responseGoogle);
    }
}
