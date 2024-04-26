using blazor_with_auth.Data;
using blazor_with_auth.Helpers;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace blazor_with_auth.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {


        public GSMTPSettings Options { get; set; }

        public EmailSender(IOptions<GSMTPSettings> config)
        {
            Options = config.Value;

        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {


            if (string.IsNullOrEmpty(Options.ApiKey))
            {
                throw new Exception("Null gsmtp api key");
            }
            //await Execute(Options.FromMail, Options.ApiKey, subject, message, toEmail);
            await Execute(Options.FromMail, Options.ApiKey, subject, message, toEmail);
        }

        public async Task Execute(string fromMail, string apiKey, string subject, string message, string toEmail)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, apiKey),
                EnableSsl = true
            };
            MailMessage GSMTPMessage = new MailMessage();
            GSMTPMessage.From = new MailAddress(fromMail);
            GSMTPMessage.Subject = subject;
            GSMTPMessage.To.Add(new MailAddress(toEmail));
            GSMTPMessage.Body = message;
            GSMTPMessage.IsBodyHtml = true;
            //  send mail
            smtpClient.Send(GSMTPMessage);
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirm your email",

             $"Please reset your password by <a href='{confirmationLink}'>clicking here</a>.");


        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,
            string resetLink) => SendEmailAsync(email, "Reset your password",
            $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email,
            string resetCode) => SendEmailAsync(email, "Reset your password",
            $"Please reset your password using the following code: {resetCode}");
    }
}