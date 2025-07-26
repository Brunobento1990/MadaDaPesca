using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Domain.Enum;

namespace MadaDaPesca.Application.ViewModel;

public class AgendaPescariaViewModel : BaseViewModel
{
    public short Dia { get; set; }
    public short Mes { get; set; }
    public short Ano { get; set; }
    public short? HoraInicial { get; set; }
    public short? HoraFinal { get; set; }
    public short? QuantidadeDePescador { get; set; }
    public string? Observacao { get; set; }
    public string DataDeAgendamento { get; set; } = string.Empty;
    public StatusAgendaPescariaEnum Status { get; set; }
    public Guid PescariaId { get; set; }
    public PescariaViewModel Pescaria { get; set; } = null!;
    public IEnumerable<GaleriaAgendaPescariaViewModel> Galeria { get; set; } = [];

    public static explicit operator AgendaPescariaViewModel(AgendaPescaria agendaPescaria)
    {
        return new AgendaPescariaViewModel
        {
            Id = agendaPescaria.Id,
            Observacao = agendaPescaria.Observacao,
            PescariaId = agendaPescaria.PescariaId,
            Status = agendaPescaria.Status,
            Dia = agendaPescaria.Dia,
            Mes = agendaPescaria.Mes,
            Ano = agendaPescaria.Ano,
            HoraInicial = agendaPescaria.HoraInicial,
            HoraFinal = agendaPescaria.HoraFinal,
            DataDeAtualizacao = agendaPescaria.DataDeAtualizacao,
            DataDeCadastro = agendaPescaria.DataDeCadastro,
            Pescaria = agendaPescaria.Pescaria != null
                ? (PescariaViewModel)agendaPescaria.Pescaria
                : null!,
            DataDeAgendamento = $"{agendaPescaria.Ano}-{agendaPescaria.Mes.ToString().PadLeft(2, '0')}-{agendaPescaria.Dia.ToString().PadLeft(2, '0')}",
            QuantidadeDePescador = agendaPescaria.QuantidadeDePescador,
            Galeria = agendaPescaria.Galeria
                .Select(g => (GaleriaAgendaPescariaViewModel)g)
        };
    }
}
