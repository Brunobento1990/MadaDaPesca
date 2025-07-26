using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class PescariaViewModel : BaseViewModel
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public short? HoraInicial { get; set; }
    public short? HoraFinal { get; set; }
    public short? QuantidadePescador { get; set; }
    public int? QuantidadeDeHorasPescaria { get; set; }
    public short? QuantidadeMaximaDeAgendamentosNoDia { get; set; }
    public bool BloquearSegundaFeira { get; set; } = false;
    public bool BloquearTercaFeira { get; set; } = false;
    public bool BloquearQuartaFeira { get; set; } = false;
    public bool BloquearQuintaFeira { get; set; } = false;
    public bool BloquearSextaFeira { get; set; } = false;
    public bool BloquearSabado { get; set; } = false;
    public bool BloquearDomingo { get; set; } = false;
    public Guid GuiaDePescaId { get; set; }
    public GuiaDePescaViewModel GuiaDePesca { get; set; } = null!;

    public static explicit operator PescariaViewModel(Pescaria pescaria)
    {
        return new PescariaViewModel
        {
            Id = pescaria.Id,
            DataDeCadastro = pescaria.DataDeCadastro,
            DataDeAtualizacao = pescaria.DataDeAtualizacao,
            Titulo = pescaria.Titulo,
            Descricao = pescaria.Descricao,
            Local = pescaria.Local,
            Valor = pescaria.Valor,
            QuantidadePescador = pescaria.QuantidadePescador,
            GuiaDePescaId = pescaria.GuiaDePescaId,
            GuiaDePesca = pescaria.GuiaDePesca == null ? null! : (GuiaDePescaViewModel)pescaria.GuiaDePesca,
            HoraFinal = pescaria.HoraFinal,
            HoraInicial = pescaria.HoraInicial,
            QuantidadeDeHorasPescaria = pescaria.QuantidadeDeHorasPescaria,
            QuantidadeMaximaDeAgendamentosNoDia = pescaria.QuantidadeMaximaDeAgendamentosNoDia,
            BloquearSegundaFeira = pescaria.BloquearSegundaFeira,
            BloquearTercaFeira = pescaria.BloquearTercaFeira,
            BloquearDomingo = pescaria.BloquearDomingo,
            BloquearQuartaFeira = pescaria.BloquearQuartaFeira,
            BloquearQuintaFeira = pescaria.BloquearQuintaFeira,
            BloquearSextaFeira = pescaria.BloquearSextaFeira,
            BloquearSabado = pescaria.BloquearSabado
        };
    }
}
