namespace MadaDaPesca.Application.ViewModel;

public class GaleriaAgendaPescariaViewModel
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public static explicit operator GaleriaAgendaPescariaViewModel(Domain.Entities.GaleriaAgendaPescaria galeria)
    {
        return new GaleriaAgendaPescariaViewModel
        {
            Id = galeria.Id,
            Url = galeria.Url
        };
    }
}
