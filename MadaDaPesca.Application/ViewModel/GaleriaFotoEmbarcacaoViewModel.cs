namespace MadaDaPesca.Application.ViewModel;

public class GaleriaFotoEmbarcacaoViewModel
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public static explicit operator GaleriaFotoEmbarcacaoViewModel(Domain.Entities.GaleriaFotoEmbarcacao galeria)
    {
        return new GaleriaFotoEmbarcacaoViewModel
        {
            Id = galeria.Id,
            Url = galeria.Url
        };
    }
}
