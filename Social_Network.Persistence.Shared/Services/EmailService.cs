using crypto;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Social_Network.Core.Application.Dtos.Email;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace Social_Network.Persistence.Shared.Services
{
    public class EmailService:IEmailService
    {
        private MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse($"{ _mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body=builder.ToMessageBody();
                using SmtpClient smtpClient = new();
                smtpClient.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtpClient.SendAsync(email);
                smtpClient.Disconnect(true);
            }catch(Exception ex)
            {

            }
        }
    }
}
