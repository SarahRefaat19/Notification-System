using Microsoft.AspNetCore.SignalR;
using NotificationSystem.Application.Dtos;
using NotificationSystem.Application.Interfaces;
using NotificationSystem.Domain;
using NotificationSystem.Insfrastructure.SignlR;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Identity.Client;
using MailKit.Security;

namespace NotificationSystem.Application.Services
{
    public class EmailNotificationService :IEmailNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public EmailNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

       public async Task SendAsync(string email, NotificationMessage message)
        {
            var mail = new MimeMessage();
            //initial from
            mail.From.Add(new MailboxAddress("BookingSystem", "sararefaat653@gmail.com"));
            //initial to
            mail.To.Add(MailboxAddress.Parse(email));
            //initial subject 
            mail.Subject = message.Title;
            //initial body
            mail.Body = new TextPart("plain")
            {
                Text = message.Body
            };
            //talk to gmail server "using to close automatic"
            using var smtp = new SmtpClient();
            //connect
            await smtp.ConnectAsync("smtp.gmail.com",587,SecureSocketOptions.StartTls);
            //auth
            await smtp.AuthenticateAsync("sararefaat653@gmail.com", "myapppassword");
            //send
            await smtp.SendAsync(mail);

            // Disconnect
            await smtp.DisconnectAsync(true);

        }
        
    }
}
