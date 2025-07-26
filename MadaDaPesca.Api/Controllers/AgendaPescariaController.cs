using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("agenda-pescaria")]
public class AgendaPescariaController : ControllerBase
{
    private readonly IAgendaPescariaService _agendaPescariaService;

    public AgendaPescariaController(IAgendaPescariaService agendaPescariaService)
    {
        _agendaPescariaService = agendaPescariaService;
    }

    [HttpPost("agendar")]
    [Autentica]
    [ProducesResponseType<AgendaPescariaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Agendar(AgendarPescariaDTO agendarPescariaDTO)
    {
        var response = await _agendaPescariaService.AgendarAsync(agendarPescariaDTO);
        return Ok(response);
    }

    [HttpPut("editar-agendamento")]
    [Autentica]
    [ProducesResponseType<AgendaPescariaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> EditarAgendamento(EditarAgendaPescariaDTO editarAgendaPescariaDTO)
    {
        var response = await _agendaPescariaService.EditarAsync(editarAgendaPescariaDTO);
        return Ok(response);
    }

    [HttpGet("agenda-do-mes")]
    [Autentica]
    [ProducesResponseType<IEnumerable<AgendaPescariaViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> AgendaDoMes([FromQuery] short mes, [FromQuery] short ano)
    {
        var agendaDoMes = await _agendaPescariaService.AgendaDoMesAsync(mes, ano);
        return Ok(agendaDoMes);
    }

    [HttpGet("obter-por-id")]
    [Autentica]
    [ProducesResponseType<AgendaPescariaViewModel>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Obter([FromQuery] Guid id)
    {
        var agenda = await _agendaPescariaService.ObterPorIdAsync(id);
        return Ok(agenda);
    }
}
