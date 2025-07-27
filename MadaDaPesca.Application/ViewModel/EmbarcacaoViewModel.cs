namespace MadaDaPesca.Application.ViewModel;

public class EmbarcacaoViewModel : BaseViewModel
{
    public string Nome { get; set; } = string.Empty;
    public string? Motor { get; set; }
    public string? MotorEletrico { get; set; }
    public string? Largura { get; set; }
    public string? Comprimento { get; set; }
    public short? QuantidadeDeLugar { get; set; }
    public Guid GuiaDePescaId { get; set; }
    public GuiaDePescaViewModel GuiaDePesca { get; set; } = null!;

    public static explicit operator EmbarcacaoViewModel(Domain.Entities.Embarcacao embarcacao)
    {
        return new EmbarcacaoViewModel
        {
            Id = embarcacao.Id,
            DataDeCadastro = embarcacao.DataDeCadastro,
            DataDeAtualizacao = embarcacao.DataDeAtualizacao,
            Nome = embarcacao.Nome,
            Motor = embarcacao.Motor,
            MotorEletrico = embarcacao.MotorEletrico,
            Largura = embarcacao.Largura,
            Comprimento = embarcacao.Comprimento,
            QuantidadeDeLugar = embarcacao.QuantidadeDeLugar,
            GuiaDePescaId = embarcacao.GuiaDePescaId,
            GuiaDePesca = embarcacao.GuiaDePesca == null ? null! : (GuiaDePescaViewModel)embarcacao.GuiaDePesca
        };
    }
}
