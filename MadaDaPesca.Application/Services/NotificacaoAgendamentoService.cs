using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Domain.Interfaces;

namespace MadaDaPesca.Application.Services;

internal class NotificacaoAgendamentoService : INotificacaoAgendamentoService
{
    private readonly IAgendaPescariaRepository _agendaPescariaRepository;
    private readonly IEmailService _emailService;

    public NotificacaoAgendamentoService(IAgendaPescariaRepository agendaPescariaRepository, IEmailService emailService)
    {
        _agendaPescariaRepository = agendaPescariaRepository;
        _emailService = emailService;
    }

    public async Task NotificarAsync()
    {
        await _emailService.EnviarAsync(new()
        {
            Assunto = "Notificação de Agendamento de Pescaria",
            Email = "brunobentocaina@gmail.com",
            Mensagem = "Enviando notificações de agendamento de pescaria.",
        });
        //var agendas = await _agendaPescariaRepository.ObterAgendaParaNotificarAsync();

        //await _emailService.EnviarAsync(new()
        //{
        //    Assunto = "Notificação de Agendamento de Pescaria",
        //    Email = "brunobentocaina@gmail.com",
        //    Mensagem = "Enviando notificações de agendamento de pescaria.",
        //});

        //if (agendas.Count == 0)
        //{
        //    return;
        //}

        //var htmlEnvio = await File.ReadAllTextAsync(Path.Combine("Htmls", "NotificacaoAgendaPescaria.html"));

        //var guias = agendas.DistinctBy(x => x.Pescaria.GuiaDePescaId).Select(x => x.Pescaria.GuiaDePescaId);

        //foreach (var guiaId in guias)
        //{
        //    var agendaDoGuia = agendas.Where(x => x.Pescaria.GuiaDePescaId == guiaId).ToList();
        //    if (agendaDoGuia.Count == 0)
        //    {
        //        continue;
        //    }
        //}
    }
}
