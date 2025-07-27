namespace MadaDaPesca.Application.DTOs;

public class PescariaDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public short? HoraInicial { get; set; }
    public short? HoraFinal { get; set; }
    public short? QuantidadePescador { get; set; }
    public short? QuantidadeMaximaDeAgendamentosNoDia { get; set; }
    public bool BloquearSegundaFeira { get; set; } = false;
    public bool BloquearTercaFeira { get; set; } = false;
    public bool BloquearQuartaFeira { get; set; } = false;
    public bool BloquearQuintaFeira { get; set; } = false;
    public bool BloquearSextaFeira { get; set; } = false;
    public bool BloquearSabado { get; set; } = false;
    public bool BloquearDomingo { get; set; } = false;
    public Guid? EmbarcacaoId { get; set; }
}

public class PescariaEditarDTO : PescariaDTO
{
    public Guid Id { get; set; }
}
