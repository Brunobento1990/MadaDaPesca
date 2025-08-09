using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class PerfilGuiaDePescaViewModel
{
    public Guid Id { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? UrlFoto { get; set; }

    public static explicit operator PerfilGuiaDePescaViewModel(GuiaDePesca guiaDePesca)
    {
        return new PerfilGuiaDePescaViewModel
        {
            Id = guiaDePesca.Id,
            UrlFoto = guiaDePesca.Pessoa.UrlFoto,
            Nome = guiaDePesca.Pessoa.Nome,
            Telefone = guiaDePesca.Pessoa.Telefone
        };
    }
}
