using ACP.Business.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(string Server, string From, string To,string Subject, string Body, string Password, int Port)
        {
            var fromAddress = new MailAddress(From);
            var fromPassword = Password;
            var toAddress = new MailAddress(To);

            // convert IdentityMessage to a MailMessage
            var email = new MailMessage(new MailAddress(From, From),
             new MailAddress(From))
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };

            using (var client = new SmtpClient(Server, Port)) // SmtpClient configuration comes from config file
            {
                client.EnableSsl = true;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                await client.SendMailAsync(email);
            }

            return true;
        }

        public async Task<bool> SendEmail(string from, string fromname, string to, string toname, string key, bool IsHtml, string body = null, string subject = null)
        {
            var client = new SendGridClient(key);
            var msg = new SendGridMessage();

            msg.From = new EmailAddress(from, fromname);
            msg.Subject = subject;
            if (IsHtml)
            {
                msg.HtmlContent = body;
            }
            else
            {
                msg.PlainTextContent = body;
            }

            msg.AddTo(new EmailAddress(to, toname));
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted ? true : false;
        }

    }
}
