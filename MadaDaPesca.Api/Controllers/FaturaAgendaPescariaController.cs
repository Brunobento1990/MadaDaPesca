using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MadaDaPesca.Api.Controllers;

[ApiController]
[Route("fatura-agenda-pescaria")]
public class FaturaAgendaPescariaController : ControllerBase
{
    private readonly IFaturaAgendaPescariaService _faturaAgendaPescariaService;
    public FaturaAgendaPescariaController(IFaturaAgendaPescariaService faturaAgendaPescariaService)
    {
        _faturaAgendaPescariaService = faturaAgendaPescariaService;
    }

    [HttpGet("obter-fatura-agenda")]
    [Autentica]
    [ProducesResponseType(typeof(FaturaAgendaPescariaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterFaturaDaAgendaAsync([FromQuery] Guid agendaPescariaId)
    {
        var fatura = await _faturaAgendaPescariaService.ObterFaturaDaAgendaAsync(agendaPescariaId);
        return Ok(fatura);
    }

    [HttpPost("gerar-fatura-agenda")]
    [Autentica]
    [ProducesResponseType(typeof(FaturaAgendaPescariaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GerarFaturaDaAgendaAsync(GerarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO)
    {
        var fatura = await _faturaAgendaPescariaService.GerarFaturaDaAgendaAsync(gerarFaturaAgendaPescariaDTO);
        return Ok(fatura);
    }

    [HttpPost("pagar-fatura-agenda")]
    [Autentica]
    [ProducesResponseType(typeof(FaturaAgendaPescariaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PagarFaturaDaAgendaAsync(PagarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO)
    {
        var fatura = await _faturaAgendaPescariaService.PagarFaturaDaAgendaAsync(gerarFaturaAgendaPescariaDTO);
        return Ok(fatura);
    }
}
