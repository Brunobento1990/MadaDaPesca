namespace MadaDaPesca.Domain.Entities;

public sealed class Pescaria : BaseEntity
{
    public Pescaria(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        string titulo,
        string descricao,
        string local,
        int tempoDeDuracao,
        Guid guiaDePescaId,
        int quantidadePescador)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Titulo = titulo;
        Descricao = descricao;
        Local = local;
        TempoDeDuracao = tempoDeDuracao;
        GuiaDePescaId = guiaDePescaId;
        QuantidadePescador = quantidadePescador;
    }

    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Local { get; private set; }
    public decimal Valor { get; private set; }
    public int TempoDeDuracao { get; private set; }
    public int QuantidadePescador { get; private set; }
    public Guid GuiaDePescaId { get; private set; }
    public GuiaDePesca GuiaDePesca { get; set; } = null!;
}
