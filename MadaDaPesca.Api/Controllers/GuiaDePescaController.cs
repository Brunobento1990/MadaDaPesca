using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
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

    [HttpPost("criar")]
    public async Task<IActionResult> CreateGuia([FromBody] GuiaDePescaCreateDTO guiaDePescaCreateDTO)
    {
        var guia = await _guiaDePescaService.CreateAsync(guiaDePescaCreateDTO);
        return Created($"/guia-de-pesca/obter-por-id?id={guia.Id}", guia);
    }
}
