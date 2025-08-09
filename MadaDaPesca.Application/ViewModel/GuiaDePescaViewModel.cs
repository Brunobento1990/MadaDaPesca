using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class GuiaDePescaViewModel : BaseViewModel
{
    public PessoaViewModel Pessoa { get; set; } = null!;
    public bool PrimeiroAcesso { get; set; }
    public IList<PescariaViewModel>? Pescarias { get; set; }
    public IList<EmbarcacaoViewModel>? Embarcacoes { get; set; }
    public IList<GaleriaDeTrofeuViewModel>? GaleriaDeTrofeu { get; set; }

    public static explicit operator GuiaDePescaViewModel(GuiaDePesca guiaDePesca)
    {
        return new GuiaDePescaViewModel
        {
            Id = guiaDePesca.Id,
            DataDeCadastro = guiaDePesca.DataDeCadastro,
            DataDeAtualizacao = guiaDePesca.DataDeAtualizacao,
            Pessoa = guiaDePesca.Pessoa == null ? null! : (PessoaViewModel)guiaDePesca.Pessoa,
            PrimeiroAcesso = guiaDePesca.AcessoGuiaDePesca?.PrimeiroAcesso ?? false,
        };
    }
}
