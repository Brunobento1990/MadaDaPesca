using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class BloqueioDataPescariaViewModel
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }

    public static explicit operator BloqueioDataPescariaViewModel(BloqueioDataPescaria bloqueioDataPescaria)
    {
        return new BloqueioDataPescariaViewModel
        {
            Id = bloqueioDataPescaria.Id,
            Data = bloqueioDataPescaria.Data
        };
    }
}
