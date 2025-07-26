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
[Route("pescaria")]
public class PescariaController : ControllerBase
{
    private readonly IPescariaService _pescariaService;

    public PescariaController(IPescariaService pescariaService)
    {
        _pescariaService = pescariaService;
    }

    [HttpPost("criar")]
    [Autentica]
    [ProducesResponseType<PescariaViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Criar(PescariaDTO pescariaDTO)
    {
        var pescaria = await _pescariaService.CriarAsync(pescariaDTO);
        return Created($"/pescaria/obter-por-id?id=${pescaria.Id}", pescaria);
    }

    [HttpPut("editar")]
    [Autentica]
    [ProducesResponseType<PescariaViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Editar(PescariaEditarDTO pescariaEditarDTO)
    {
        var pescaria = await _pescariaService.EditarAsync(pescariaEditarDTO);
        return Ok(pescaria);
    }

    [HttpGet("obter-por-id")]
    [Autentica]
    [ProducesResponseType<PescariaViewModel>(201)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Obter([FromQuery] Guid id)
    {
        var pescaria = await _pescariaService.ObterPorIdAsync(id);
        return Ok(pescaria);
    }

    [HttpDelete("delete")]
    [Autentica]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await _pescariaService.ExcluirAsync(id);
        return Ok(new
        {
            Resultado = true
        });
    }

    [HttpGet("paginacao")]
    [Autentica]
    [ProducesResponseType<PaginacaoModel<PescariaViewModel>>(200)]
    [ProducesResponseType<ErrorViewModel>(400)]
    public async Task<IActionResult> Paginacao([FromQuery] PescariaPaginacao pescariaPaginacao)
    {
        var pescarias = await _pescariaService.PaginacaoAsync(pescariaPaginacao);
        return Ok(pescarias);
    }
}
