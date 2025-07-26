using MadaDaPesca.Domain.Exceptions;
using MadaDaPesca.Domain.Extensions;
using System.Threading;

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
        Guid guiaDePescaId,
        short? quantidadePescador,
        decimal valor,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeMaximaDeAgendamentosNoDia,
        bool bloquearSegundaFeira,
        bool bloquearTercaFeira,
        bool bloquearQuartaFeira,
        bool bloquearQuintaFeira,
        bool bloquearSextaFeira,
        bool bloquearSabado,
        bool bloquearDomingo)
            : base(id, dataDeCadastro, dataDeAtualizacao, excluido)
    {
        Titulo = titulo;
        Descricao = descricao;
        Local = local;
        GuiaDePescaId = guiaDePescaId;
        QuantidadePescador = quantidadePescador;
        Valor = valor;
        HoraInicial = horaInicial;
        HoraFinal = horaFinal;
        QuantidadeMaximaDeAgendamentosNoDia = quantidadeMaximaDeAgendamentosNoDia;
        BloquearSegundaFeira = bloquearSegundaFeira;
        BloquearTercaFeira = bloquearTercaFeira;
        BloquearQuartaFeira = bloquearQuartaFeira;
        BloquearQuintaFeira = bloquearQuintaFeira;
        BloquearSextaFeira = bloquearSextaFeira;
        BloquearSabado = bloquearSabado;
        BloquearDomingo = bloquearDomingo;
    }

    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Local { get; private set; }
    public decimal Valor { get; private set; }
    public short? HoraInicial { get; private set; }
    public short? HoraFinal { get; private set; }
    public short? QuantidadePescador { get; private set; }
    public short? QuantidadeMaximaDeAgendamentosNoDia { get; private set; }
    public bool BloquearSegundaFeira { get; private set; } = false;
    public bool BloquearTercaFeira { get; private set; } = false;
    public bool BloquearQuartaFeira { get; private set; } = false;
    public bool BloquearQuintaFeira { get; private set; } = false;
    public bool BloquearSextaFeira { get; private set; } = false;
    public bool BloquearSabado { get; private set; } = false;
    public bool BloquearDomingo { get; private set; } = false;
    public int? QuantidadeDeHorasPescaria
    {
        get
        {
            if (!HoraInicial.HasValue || !HoraFinal.HasValue)
            {
                return null;
            }

            return HoraFinal.Value - HoraInicial.Value;
        }
    }
    public Guid GuiaDePescaId { get; private set; }
    public GuiaDePesca GuiaDePesca { get; set; } = null!;
    public IList<AgendaPescaria> Agendamentos { get; set; } = [];

    public void Excluir()
    {
        Excluido = true;
        DataDeAtualizacao = DateTime.UtcNow;
    }

    public void Editar(
        string titulo,
        string descricao,
        string local,
        decimal valor,
        short? quantidadeDePescador,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeMaximaDeAgendamentosNoDia,
        bool bloquearSegundaFeira,
        bool bloquearTercaFeira,
        bool bloquearQuartaFeira,
        bool bloquearQuintaFeira,
        bool bloquearSextaFeira,
        bool bloquearSabado,
        bool bloquearDomingo)
    {
        Titulo = titulo;
        Descricao = descricao;
        Local = local;
        Valor = valor;
        QuantidadePescador = quantidadeDePescador;
        HoraFinal = horaFinal;
        HoraInicial = horaInicial;
        QuantidadeMaximaDeAgendamentosNoDia = quantidadeMaximaDeAgendamentosNoDia;
        BloquearSegundaFeira = bloquearSegundaFeira;
        BloquearTercaFeira = bloquearTercaFeira;
        BloquearQuartaFeira = bloquearQuartaFeira;
        BloquearQuintaFeira = bloquearQuintaFeira;
        BloquearSextaFeira = bloquearSextaFeira;
        BloquearSabado = bloquearSabado;
        BloquearDomingo = bloquearDomingo;

        Validar();
    }

    public static Pescaria Criar(
        string titulo,
        string descricao,
        string local,
        decimal valor,
        short? quantidadePescador,
        Guid guiaDePescaId,
        short? horaInicial,
        short? horaFinal,
        short? quantidadeMaximaDeAgendamentosNoDia,
        bool bloquearSegundaFeira,
        bool bloquearTercaFeira,
        bool bloquearQuartaFeira,
        bool bloquearQuintaFeira,
        bool bloquearSextaFeira,
        bool bloquearSabado,
        bool bloquearDomingo)
    {
        var pescaria = new Pescaria(
            id: Guid.NewGuid(),
            dataDeCadastro: DateTime.UtcNow,
            dataDeAtualizacao: DateTime.UtcNow,
            excluido: false,
            titulo: titulo,
            descricao: descricao,
            local: local,
            guiaDePescaId: guiaDePescaId,
            quantidadePescador: quantidadePescador,
            valor: valor,
            horaInicial: horaInicial,
            horaFinal: horaFinal,
            quantidadeMaximaDeAgendamentosNoDia: quantidadeMaximaDeAgendamentosNoDia,
            bloquearSegundaFeira: bloquearSegundaFeira,
            bloquearTercaFeira: bloquearTercaFeira,
            bloquearQuartaFeira: bloquearQuartaFeira,
            bloquearQuintaFeira: bloquearQuintaFeira,
            bloquearSextaFeira: bloquearSextaFeira,
            bloquearSabado: bloquearSabado,
            bloquearDomingo: bloquearDomingo);

        pescaria.Validar();

        return pescaria;
    }

    public void Validar()
    {
        Titulo = Titulo.ValidarNull("O título da pescaria não pode ser nulo ou vazio.")
            .ValidarLength(100, "O título da pescaria deve ter no máximo 100 caracteres.");

        Descricao = Descricao.ValidarNull("A descrição da pescaria não pode ser nula ou vazia.")
            .ValidarLength(255, "A descrição da pescaria deve ter no máximo 255 caracteres.");

        Local = Local.ValidarNull("O local da pescaria não pode ser nulo ou vazio.")
            .ValidarLength(100, "O local da pescaria deve ter no máximo 100 caracteres.");

        if (HoraInicial.HasValue)
        {
            if (HoraInicial.Value > 24)
            {
                throw new ValidacaoException("A hora inicial não pode ser maior que 24");
            }

            if (HoraInicial.Value < 0)
            {
                throw new ValidacaoException("A hora inicial não pode ser menor que zero");
            }
        }

        if (HoraFinal.HasValue)
        {
            if (HoraFinal.Value < 0)
            {
                throw new ValidacaoException("Não é possível informar uma hora final menor que zero");
            }

            if (!HoraInicial.HasValue)
            {
                throw new ValidacaoException("Não é possível informar uma hora final sem uma hora inicial");
            }

            if (HoraFinal.Value > 24)
            {
                throw new ValidacaoException("A hora final não pode ser maior que 24");
            }

            if (HoraInicial.Value > HoraFinal.Value)
            {
                throw new ValidacaoException("A hora final não pode ser maior que a hora inicial");
            }
        }

        if (QuantidadePescador < 0)
        {
            throw new ValidacaoException("A quantidade de pescadores não pode ser menor que zero.");
        }
    }

    public void EstaDisponivelParaAgendamento(DateTime dataDeAgendamento)
    {
        if (dataDeAgendamento == DateTime.MinValue)
        {
            throw new ValidacaoException("A data de agendamento inválida.");
        }

        if (dataDeAgendamento.Date < DateTime.UtcNow.Date)
        {
            throw new ValidacaoException("Não é possível agendar uma pescaria para uma data anterior a hoje.");
        }
        var diaDaSemana = (int)dataDeAgendamento.DayOfWeek;
        if ((diaDaSemana == 0 && BloquearDomingo) ||
            (diaDaSemana == 1 && BloquearSegundaFeira) ||
            (diaDaSemana == 2 && BloquearTercaFeira) ||
            (diaDaSemana == 3 && BloquearQuartaFeira) ||
            (diaDaSemana == 4 && BloquearQuintaFeira) ||
            (diaDaSemana == 5 && BloquearSextaFeira) ||
            (diaDaSemana == 6 && BloquearSabado))
        {
            switch (diaDaSemana)
            {
                case 0:
                    throw new ValidacaoException("Não é possível agendar pescaria aos domingos.");
                case 1:
                    throw new ValidacaoException("Não é possível agendar pescaria às segundas-feiras.");
                case 2:
                    throw new ValidacaoException("Não é possível agendar pescaria às terças-feiras.");
                case 3:
                    throw new ValidacaoException("Não é possível agendar pescaria às quartas-feiras.");
                case 4:
                    throw new ValidacaoException("Não é possível agendar pescaria às quintas-feiras.");
                case 5:
                    throw new ValidacaoException("Não é possível agendar pescaria às sextas-feiras.");
                case 6:
                    throw new ValidacaoException("Não é possível agendar pescaria aos sábados.");
            }
        }
    }
}
