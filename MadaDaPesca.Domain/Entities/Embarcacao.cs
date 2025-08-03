
using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;

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
    public IList<GaleriaFotoEmbarcacao>? Galeria { get; set; }

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

        Validar();
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
        var embarcacao = new Embarcacao(
            Guid.NewGuid(),
            DateTime.Now,
            DateTime.Now,
            false,
            nome,
            motor,
            motorEletrico,
            largura,
            comprimento,
            quantidadeDeLugar,
            guiaDePescaId);

        embarcacao.Validar();

        return embarcacao;
    }

    public void Validar()
    {
        Nome = Nome.ValidarNull("Informe o nome")
            .ValidarLength(255, "O nome deve conter no máximo 100 caracteres");
        Motor = Motor?.ValidarLengthNull(255, "O motor deve conter no máximo 255 caracteres");
        MotorEletrico = MotorEletrico?.ValidarLengthNull(255, "O motor elétrico deve conter no máximo 255 caracteres");
        Largura = Largura?.ValidarLengthNull(255, "A largura deve conter no máximo 255 caracteres");
        Comprimento = Comprimento?.ValidarLengthNull(255, "O comprimento deve conter no máximo 255 caracteres");

        if (QuantidadeDeLugar <= 0)
        {
            throw new ValidacaoException("A quantidade de lugares deve ser maior que zero.");
        }
    }
}
