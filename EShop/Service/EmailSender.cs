using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EShop.Service
{
    public class EmailSender : IEmailSenderTwo
    {
        public async Task SendEmailAsync(string email, string subject, string content)
        {

            try
            {

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("EShop Admin", "postthecreater@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("EShop Customer", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = content
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("postthecreater@gmail.com", "salam023");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
