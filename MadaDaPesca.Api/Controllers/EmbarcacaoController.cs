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
[Route("embarcacao")]
public class EmbarcacaoController : ControllerBase
{
    private readonly IEmbarcacaoService _embarcacaoService;
    public EmbarcacaoController(IEmbarcacaoService embarcacaoService)
    {
        _embarcacaoService = embarcacaoService;
    }

    [HttpPost("criar")]
    [Autentica]
    [ProducesResponseType<EmbarcacaoViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Criar(EmbarcacaoCreateDTO embarcacaoCreateDTO)
    {
        var embarcacao = await _embarcacaoService.CriarAsync(embarcacaoCreateDTO);
        return Created($"/embarcacao/obter-por-id?id=${embarcacao.Id}", embarcacao);
    }

    [HttpPut("editar")]
    [Autentica]
    [ProducesResponseType<EmbarcacaoViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Editar(EmbarcacaoEditarDTO embarcacaoEditarDTO)
    {
        var embarcacao = await _embarcacaoService.EditarAsync(embarcacaoEditarDTO);
        return Ok(embarcacao);
    }

    [HttpGet("obter-por-id")]
    [Autentica]
    [ProducesResponseType<EmbarcacaoViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Obter([FromQuery] Guid id)
    {
        var embarcacao = await _embarcacaoService.ObterPorIdAsync(id);
        return Ok(embarcacao);
    }

    [HttpDelete("delete")]
    [Autentica]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await _embarcacaoService.ExcluirAsync(id);
        return Ok(new
        {
            Resultado = true
        });
    }

    [HttpGet("paginacao")]
    [Autentica]
    [ProducesResponseType<PaginacaoModel<EmbarcacaoViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Paginacao([FromQuery] EmbarcacaoPaginacao embarcacaoPaginacao)
    {
        var embarcacoes = await _embarcacaoService.PaginacaoAsync(embarcacaoPaginacao);
        return Ok(embarcacoes);
    }
}
