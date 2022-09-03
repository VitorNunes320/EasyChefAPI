using Domain.Helpers;
using Microsoft.Extensions.Options;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; set; }
        private readonly SmtpClient smtp;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            // smtp = new SmtpClient
            // {
            //     Host = _emailSettings.Host,
            //     Port = _emailSettings.Port,
            //     EnableSsl = true,
            //     Credentials = new NetworkCredential(_emailSettings.SMTP_ID, _emailSettings.SMTP_PWD)
            // };
        }

        public bool SendEmail(string receiver, string subject, string content)
        {

                MailMessage message = new MailMessage(_emailSettings.From, receiver)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    From = new MailAddress(_emailSettings.From, _emailSettings.Name)
                };

                message.Body = content;
                smtp.Send(message);
                return true;

        }
    }
}
