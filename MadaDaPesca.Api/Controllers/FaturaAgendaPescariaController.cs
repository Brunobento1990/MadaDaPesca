using MadaDaPesca.Api.Attributes;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Models;
using MadaDaPesca.Infra.Models;
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

    [HttpGet("obter-por-id")]
    [Autentica]
    [ProducesResponseType(typeof(FaturaAgendaPescariaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterPorIdAsync([FromQuery] Guid id)
    {
        var fatura = await _faturaAgendaPescariaService.ObterPorIdAsync(id);
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

    [HttpPost("estornar-fatura-agenda")]
    [Autentica]
    [ProducesResponseType(typeof(FaturaAgendaPescariaViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EstornarFaturaDaAgendaAsync(EstornarFaturaAgendaPescariaDTO gerarFaturaAgendaPescariaDTO)
    {
        var fatura = await _faturaAgendaPescariaService.EstornarFaturaDaAgendaAsync(gerarFaturaAgendaPescariaDTO);
        return Ok(fatura);
    }

    [HttpGet("paginacao")]
    [Autentica]
    [ProducesResponseType<PaginacaoModel<FaturaAgendaPescariaViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Paginacao([FromQuery] FaturaPaginacao faturaPaginacao)
    {
        var faturas = await _faturaAgendaPescariaService.PaginacaoAsync(faturaPaginacao);
        return Ok(faturas);
    }
}
