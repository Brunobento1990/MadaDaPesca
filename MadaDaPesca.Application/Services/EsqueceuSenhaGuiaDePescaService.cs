using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Application.Services;

internal class EsqueceuSenhaGuiaDePescaService : IEsqueceuSenhaGuiaDePescaService
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;

    public EsqueceuSenhaGuiaDePescaService(IEmailService emailService, IConfiguration configuration, IGuiaDePescaRepository guiaDePescaRepository)
    {
        _emailService = emailService;
        _configuration = configuration;
        _guiaDePescaRepository = guiaDePescaRepository;
    }

    public async Task EsqueceuSenhaAsync(EsqueceSenhaDTO esqueceSenhaDTO)
    {
        var guia = await _guiaDePescaRepository.ObterPorCpfAsync(esqueceSenhaDTO.Cpf.LimparMascaraCpf())
            ?? throw new ValidacaoException("Não foi possível localizar o seu cadastro");

        guia.AcessoGuiaDePesca.EsqueceuSenha();

        var htmlEnvio = await File.ReadAllTextAsync(Path.Combine("Htmls", "EsqueceuSenha.html"));

        htmlEnvio = htmlEnvio.Replace("***usuario***", guia.Pessoa.Nome);
        htmlEnvio = htmlEnvio.Replace("***link***", $"{_configuration["UrlFront"]}/recuperar-senha/{guia.AcessoGuiaDePesca.TokenEsqueceuSenha}");

        var resultadoEmail = await _emailService.EnviarAsync(new()
        {
            Email = guia.Pessoa.Email,
            Assunto = "Recuperar senha",
            Html = htmlEnvio
        });

        if (!resultadoEmail)
        {
            throw new ValidacaoException("Não foi possível enviar o e-mail de recuperação de senha, verifique se o e-mail está correto");
        }

        _guiaDePescaRepository.Editar(guia);
        await _guiaDePescaRepository.SaveChangesAsync();
    }
}
