namespace MadaDaPesca.Domain.Entities;

public sealed class GuiaDePesca : BaseEntity
{
    public GuiaDePesca(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        Guid pessoaId,
        Guid acessoGuiaDePescaId)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        PessoaId = pessoaId;
        AcessoGuiaDePescaId = acessoGuiaDePescaId;
    }

    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; set; } = null!;
    public Guid AcessoGuiaDePescaId { get; private set; }
    public AcessoGuiaDePesca AcessoGuiaDePesca { get; set; } = null!;
    public IList<Pescaria>? Pescarias { get; set; }
}
