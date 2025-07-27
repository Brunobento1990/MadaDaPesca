using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Application.Services;

internal class GuiaDePescaService : IGuiaDePescaService
{
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IUploadImagemService _uploadImagemService;
    public GuiaDePescaService(IGuiaDePescaRepository guiaDePescaRepository, IEmailService emailService, IConfiguration configuration, IUploadImagemService uploadImagemService)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
        _emailService = emailService;
        _configuration = configuration;
        _uploadImagemService = uploadImagemService;
    }

    public async Task<GuiaDePescaViewModel> CreateAsync(GuiaDePescaCreateDTO guiaDePescaCreateDTO)
    {
        var guiaExistente = await _guiaDePescaRepository
            .ObterParaValidarAsync(guiaDePescaCreateDTO.Cpf.LimparMascaraCpf(), guiaDePescaCreateDTO.Email.Trim());

        if (guiaExistente != null)
        {
            var erros = new List<string>();
            if (guiaExistente.Pessoa.Cpf == guiaDePescaCreateDTO.Cpf.LimparMascaraCpf())
            {
                erros.Add("Já existe um guia de pesca cadastrado com o CPF informado.");
            }
            if (guiaExistente.Pessoa.Email == guiaDePescaCreateDTO.Email.Trim())
            {
                erros.Add("Já existe um guia de pesca cadastrado com o e-mail informado.");
            }
            throw new ValidacaoException(erros);
        }

        string? urlFoto = guiaDePescaCreateDTO.UrlFoto;
        var id = Guid.NewGuid();

        if (!string.IsNullOrEmpty(guiaDePescaCreateDTO.UrlFoto) && !guiaDePescaCreateDTO.UrlFoto.ToLower().StartsWith("http"))
        {
            urlFoto = await _uploadImagemService.UploadAsync(guiaDePescaCreateDTO.UrlFoto, id);
        }

        var guia = GuiaDePesca.Novo(cpf: guiaDePescaCreateDTO.Cpf.LimparMascaraCpf(),
                                nome: guiaDePescaCreateDTO.Nome.Trim(),
                                telefone: guiaDePescaCreateDTO.Telefone.LimparMascaraTelefone(),
                                email: guiaDePescaCreateDTO.Email.Trim(),
                                senha: PasswordAdapter.GenerateHash(guiaDePescaCreateDTO.Senha),
                                urlFoto: urlFoto,
                                id: id,
                                aceitoDeTermos: guiaDePescaCreateDTO.AceitoDeTermos);

        await _guiaDePescaRepository.AddAsync(guia);
        await _guiaDePescaRepository.SaveChangesAsync();

        var htmlEnvio = await File.ReadAllTextAsync(Path.Combine("Htmls", "NovoGuiaDePescaHtml.html"));

        htmlEnvio = htmlEnvio.Replace("***usuario***", guia.Pessoa.Nome);
        htmlEnvio = htmlEnvio.Replace("***link***", $"{_configuration["UrlFront"]}/confirmar-conta/{guia.Id}");

        var resultadoEmail = await _emailService.EnviarAsync(new()
        {
            Email = guia.Pessoa.Email,
            Assunto = "Confirme seu e-mail para finalizar seu cadastro",
            Html = htmlEnvio
        });

        if (!resultadoEmail)
        {
            throw new ValidacaoException("Não foi possível enviar seu e-mail de confirmação de conta, verifique se e-mail");
        }

        return (GuiaDePescaViewModel)guia;
    }
}