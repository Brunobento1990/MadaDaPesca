using MadaDaPesca.Application.DTOs;
using MadaDaPesca.Application.Interfaces;
using MadaDaPesca.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace MadaDaPesca.Infra.Services;

internal class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> EnviarAsync(EnvioEmailDTO envioEmailDTO)
    {
        try
        {
            var email = _configuration["EmailConfig:Email"]!;
            var servidor = _configuration["EmailConfig:Servidor"]!;
            var senha = _configuration["EmailConfig:Senha"]!;
            var porta = int.Parse(_configuration["EmailConfig:Porta"]!);

            var mail = new MailMessage(email, envioEmailDTO.Email)
            {
                Subject = envioEmailDTO.Assunto,
                SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8"),
                BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8"),
                Body = envioEmailDTO.Mensagem
            };

            if (envioEmailDTO.Arquivo != null && !string.IsNullOrWhiteSpace(envioEmailDTO.NomeDoArquivo) && !string.IsNullOrWhiteSpace(envioEmailDTO.TipoDoArquivo))
            {
                var anexo = new Attachment(new MemoryStream(envioEmailDTO.Arquivo), envioEmailDTO.NomeDoArquivo, envioEmailDTO.TipoDoArquivo);
                mail.Attachments.Add(anexo);
            }

            if (!string.IsNullOrWhiteSpace(envioEmailDTO.Html))
            {
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(envioEmailDTO.Html, null, MediaTypeNames.Text.Html));
            }

            var smtp = new SmtpClient(servidor, porta)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, senha)
            };
            await smtp.SendMailAsync(mail);

            return true;
        }
        catch (Exception ex)
        {
            LogService.LogApi("Erro ao enviar email", ex);

            return false;
        }
    }
}
