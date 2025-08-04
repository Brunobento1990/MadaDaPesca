
using MadaDaPesca.Domain.Extensions;

namespace MadaDaPesca.Domain.Entities;

public sealed class GaleriaDeTrofeu : BaseEntity
{
    public GaleriaDeTrofeu(Guid id, DateTime dataDeCadastro, DateTime dataDeAtualizacao, bool excluido, Guid guiaDePescaId, string url, string? descricao) : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        GuiaDePescaId = guiaDePescaId;
        Url = url;
        Descricao = descricao;
    }

    public Guid GuiaDePescaId { get; private set; }
    public GuiaDePesca GuiaDePesca { get; set; } = null!;
    public string Url { get; private set; }
    public string? Descricao { get; private set; }

    public static GaleriaDeTrofeu Novo(Guid guiaDePescaId, string url, string? descricao, Guid id)
    {
        var galeria = new GaleriaDeTrofeu(
            id: id,
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            guiaDePescaId: guiaDePescaId,
            url: url,
            descricao: descricao);

        galeria.Validar();

        return galeria;
    }

    public void Validar()
    {
        Descricao = Descricao.ValidarLengthNull(255, "Descrição não pode ter mais de 255 caracteres.");
    }
}
