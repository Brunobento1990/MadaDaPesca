using MadaDaPesca.Domain.Entities;

namespace MadaDaPesca.Application.ViewModel;

public class GaleriaDeTrofeuViewModel : BaseViewModel
{
    public string Url { get; set; } = string.Empty;
    public string? Descricao { get; set; }

    public static explicit operator GaleriaDeTrofeuViewModel(GaleriaDeTrofeu galeriaDeTrofeu)
    {
        return new GaleriaDeTrofeuViewModel
        {
            Id = galeriaDeTrofeu.Id,
            DataDeAtualizacao = galeriaDeTrofeu.DataDeAtualizacao,
            DataDeCadastro = galeriaDeTrofeu.DataDeCadastro,
            Descricao = galeriaDeTrofeu.Descricao,
            Url = galeriaDeTrofeu.Url
        };
    }
}
