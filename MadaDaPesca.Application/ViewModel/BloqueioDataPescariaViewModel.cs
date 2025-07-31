using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class BloqueioDataPescariaViewModel
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public short Ano { get; set; }
    public short Mes { get; set; }
    public short Dia { get; set; }
    public string Titulo { get; set; } = "Data Bloqueada";

    public static explicit operator BloqueioDataPescariaViewModel(BloqueioDataPescaria bloqueioDataPescaria)
    {
        return new BloqueioDataPescariaViewModel
        {
            Id = bloqueioDataPescaria.Id,
            Data = bloqueioDataPescaria.Data,
            Ano = (short)bloqueioDataPescaria.Data.Year,
            Mes = (short)bloqueioDataPescaria.Data.Month,
            Dia = (short)bloqueioDataPescaria.Data.Day
        };
    }
}
