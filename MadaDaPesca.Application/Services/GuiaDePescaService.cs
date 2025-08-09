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
    private readonly IGuiaDePescaLogado _guiaDePescaLogado;
    public GuiaDePescaService(IGuiaDePescaRepository guiaDePescaRepository, IEmailService emailService, IConfiguration configuration, IUploadImagemService uploadImagemService, IGuiaDePescaLogado guiaDePescaLogado)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
        _emailService = emailService;
        _configuration = configuration;
        _uploadImagemService = uploadImagemService;
        _guiaDePescaLogado = guiaDePescaLogado;
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
        Guid? idFoto = null;

        if (!string.IsNullOrEmpty(guiaDePescaCreateDTO.UrlFoto) && !guiaDePescaCreateDTO.UrlFoto.ToLower().StartsWith("http"))
        {
            idFoto = Guid.NewGuid();
            urlFoto = await _uploadImagemService.UploadAsync(guiaDePescaCreateDTO.UrlFoto, idFoto.Value);
        }

        var guia = GuiaDePesca.Novo(cpf: guiaDePescaCreateDTO.Cpf.LimparMascaraCpf(),
                                nome: guiaDePescaCreateDTO.Nome.Trim(),
                                telefone: guiaDePescaCreateDTO.Telefone.LimparMascaraTelefone(),
                                email: guiaDePescaCreateDTO.Email.Trim(),
                                senha: PasswordAdapter.GenerateHash(guiaDePescaCreateDTO.Senha),
                                urlFoto: urlFoto,
                                aceitoDeTermos: guiaDePescaCreateDTO.AceitoDeTermos,
                                idFoto: idFoto);

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

    public async Task<GuiaDePescaViewModel> EditarMinhaContaAsync(GuiaDePescaEditarDTO guiaDePescaEditarDTO)
    {
        var guia = await _guiaDePescaRepository.ObterPorIdAsync(_guiaDePescaLogado.Id)
           ?? throw new ValidacaoException("Guia de pesca não encontrado.");

        if (guia.Pessoa.Email != guiaDePescaEditarDTO.Email.Trim())
        {
            var guiaExistente = await _guiaDePescaRepository
                .ObterParaValidarAsync(guia.Pessoa.Cpf, guiaDePescaEditarDTO.Email.Trim(), guia.Id);
            if (guiaExistente != null)
            {
                throw new ValidacaoException("Já existe um guia de pesca cadastrado com o e-mail informado.");
            }

            var htmlEnvio = await File.ReadAllTextAsync(Path.Combine("Htmls", "ConfirmarEmail.html"));

            htmlEnvio = htmlEnvio.Replace("***usuario***", guia.Pessoa.Nome);
            htmlEnvio = htmlEnvio.Replace("***link***", $"{_configuration["UrlFront"]}/confirmar-conta/{guia.Id}");

            var resultadoEmail = await _emailService.EnviarAsync(new()
            {
                Email = guiaDePescaEditarDTO.Email,
                Assunto = "Confirme seu e-mail",
                Html = htmlEnvio
            });

            if (!resultadoEmail)
            {
                throw new ValidacaoException("Não foi possível enviar seu e-mail de confirmação de conta, verifique se e-mail");
            }

            guia.AcessoGuiaDePesca.VerificarEmail(false);
        }

        if (!string.IsNullOrWhiteSpace(guiaDePescaEditarDTO.UrlFoto)
                && !guiaDePescaEditarDTO.UrlFoto.StartsWith("http"))
        {
            var idFoto = Guid.NewGuid();
            guiaDePescaEditarDTO.UrlFoto = await _uploadImagemService.UploadAsync(guiaDePescaEditarDTO.UrlFoto, idFoto);

            if (guia.Pessoa.IdFoto.HasValue)
            {
                await _uploadImagemService.DeleteAsync(guia.Pessoa.IdFoto.Value);
            }

            guia.Pessoa.EditarFoto(guiaDePescaEditarDTO.UrlFoto, idFoto);
        }

        guia.Editar(
            nome: guiaDePescaEditarDTO.Nome.Trim(),
            email: guiaDePescaEditarDTO.Email.Trim(),
            telefone: guiaDePescaEditarDTO.Telefone.LimparMascaraTelefone());

        _guiaDePescaRepository.Editar(guia);
        await _guiaDePescaRepository.SaveChangesAsync();

        return (GuiaDePescaViewModel)guia;
    }

    public async Task<GuiaDePescaViewModel> MinhaContaAsync()
    {
        var guia = await _guiaDePescaRepository.ObterPorIdAsync(_guiaDePescaLogado.Id)
            ?? throw new ValidacaoException("Guia de pesca não encontrado.");

        return (GuiaDePescaViewModel)guia;
    }

    public async Task<GuiaDePescaViewModel> ObterPerfilAsyncAsync(Guid id)
    {
        var guia = await _guiaDePescaRepository.ObterPoraPerfilAsync(id)
            ?? throw new ValidacaoException("Guia de pesca não encontrado.");

        var guiaViewModel = (GuiaDePescaViewModel)guia;
        guiaViewModel.Pescarias = [];
        guiaViewModel.Embarcacoes = [];
        guiaViewModel.GaleriaDeTrofeu = [];
        guiaViewModel.Pessoa.Cpf = null!;

        if (guia.Pescarias?.Count > 0)
        {
            foreach (var item in guia.Pescarias)
            {
                var pescariaViewModel = (PescariaViewModel)item;

                pescariaViewModel.GuiaDePesca = null!;
                guiaViewModel.Pescarias.Add(pescariaViewModel);
            }
        }

        if (guia.GaleriaDeTrofeu?.Count > 0)
        {
            foreach (var item in guia.GaleriaDeTrofeu)
            {
                var galeriaDeTrofeuViewModel = (GaleriaDeTrofeuViewModel)item;
                guiaViewModel.GaleriaDeTrofeu.Add(galeriaDeTrofeuViewModel);
            }
        }

        return guiaViewModel;
    }
}