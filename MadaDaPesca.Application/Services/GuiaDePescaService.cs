using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class GuiaDePescaService : IGuiaDePescaService
{
    private readonly IGuiaDePescaRepository _guiaDePescaRepository;

    public GuiaDePescaService(IGuiaDePescaRepository guiaDePescaRepository)
    {
        _guiaDePescaRepository = guiaDePescaRepository;
    }

    public async Task<GuiaDePescaViewModel> CreateAsync(GuiaDePescaCreateDTO guiaDePescaCreateDTO)
    {
        guiaDePescaCreateDTO.Validar();

        var guiaExistente = await _guiaDePescaRepository
            .ObterParaValidarAsync(guiaDePescaCreateDTO.Cpf.LimparMascaraCpf(), guiaDePescaCreateDTO.Email.Trim());

        if (guiaExistente != null)
        {
            if (guiaExistente.Pessoa.Cpf == guiaDePescaCreateDTO.Cpf.LimparMascaraCpf())
            {
                throw new ValidacaoException("Já existe um guia de pesca cadastrado com o CPF informado.");
            }
            if (guiaExistente.Pessoa.Email == guiaDePescaCreateDTO.Email.Trim())
            {
                throw new ValidacaoException("Já existe um guia de pesca cadastrado com o e-mail informado.");
            }
        }

        var guia = GuiaDePesca.Novo(cpf: guiaDePescaCreateDTO.Cpf.LimparMascaraCpf(),
                                nome: guiaDePescaCreateDTO.Nome.Trim(),
                                telefone: guiaDePescaCreateDTO.Telefone.LimparMascaraTelefone(),
                                email: guiaDePescaCreateDTO.Email.Trim(),
                                senha: PasswordAdapter.GenerateHash(guiaDePescaCreateDTO.Senha));

        await _guiaDePescaRepository.AddAsync(guia);
        await _guiaDePescaRepository.SaveChangesAsync();

        return (GuiaDePescaViewModel)guia;
    }
}