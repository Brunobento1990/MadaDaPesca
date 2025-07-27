
namespace MadaDaPesca.Domain.Entities;

public sealed class Embarcacao : BaseEntity
{
    public Embarcacao(
        Guid id,
        DateTime dataDeCadastro,
        DateTime dataDeAtualizacao,
        bool excluido,
        string nome,
        string? motor,
        string? motorEletrico,
        string? largura,
        string? comprimento,
        short? quantidadeDeLugar,
        Guid guiaDePescaId)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Nome = nome;
        Motor = motor;
        MotorEletrico = motorEletrico;
        Largura = largura;
        Comprimento = comprimento;
        QuantidadeDeLugar = quantidadeDeLugar;
        GuiaDePescaId = guiaDePescaId;
    }

    public string Nome { get; private set; }
    public string? Motor { get; private set; }
    public string? MotorEletrico { get; private set; }
    public string? Largura { get; private set; }
    public string? Comprimento { get; private set; }
    public short? QuantidadeDeLugar { get; private set; }
    public Guid GuiaDePescaId { get; private set; }
    public GuiaDePesca GuiaDePesca { get; set; } = null!;
    public IList<Pescaria>? Pescarias { get; set; }

    public void Editar(
        string nome,
        string? motor,
        string? motorEletrico,
        string? largura,
        string? comprimento,
        short? quantidadeDeLugar)
    {
        Nome = nome;
        Motor = motor;
        MotorEletrico = motorEletrico;
        Largura = largura;
        Comprimento = comprimento;
        QuantidadeDeLugar = quantidadeDeLugar;
    }

    public void Excluir()
    {
        Excluido = true;
    }

    public static Embarcacao Criar(
        string nome,
        string? motor,
        string? motorEletrico,
        string? largura,
        string? comprimento,
        short? quantidadeDeLugar,
        Guid guiaDePescaId)
    {
        return new Embarcacao(
            Guid.NewGuid(),
            DateTime.UtcNow,
            DateTime.UtcNow,
            false,
            nome,
            motor,
            motorEletrico,
            largura,
            comprimento,
            quantidadeDeLugar,
            guiaDePescaId);
    }
}
