using MadaDaPesca.Application.Adapters;
using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.ViewModel;
using MadaDaPesca.Domain.Entities;
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

        var guia = GuiaDePesca.Novo(cpf: guiaDePescaCreateDTO.Cpf,
                                    nome: guiaDePescaCreateDTO.Nome,
                                    telefone: guiaDePescaCreateDTO.Telefone,
                                    email: guiaDePescaCreateDTO.Email,
                                    senha: PasswordAdapter.GenerateHash(guiaDePescaCreateDTO.Senha));

        await _guiaDePescaRepository.AddAsync(guia);
        await _guiaDePescaRepository.SaveChangesAsync();

        return (GuiaDePescaViewModel)guia;
    }
}
