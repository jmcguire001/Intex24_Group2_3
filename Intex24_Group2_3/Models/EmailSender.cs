using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Darla.Models
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false);
    }

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
        {
            string MailServer = _configuration["EmailSettings:MailServer"];
            string FromEmail = _configuration["EmailSettings:FromEmail"];
            string Password = _configuration["EmailSettings:Password"];
            int MailPort = int.Parse(_configuration["EmailSettings:MailPort"]);


            var client = new SmtpClient(MailServer, MailPort)
            {
                Credentials = new NetworkCredential(FromEmail, Password),
                EnableSsl = true,
            };

            MailMessage mailMessage = new MailMessage(FromEmail, ToEmail, Subject, Body)
            {
                IsBodyHtml = IsBodyHtml
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}

//using System.Net.Mail;
//using System.Net;

//namespace Intex24_Group2_3.Models
//{
//    public interface ISenderEmail
//    {
//        Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false);
//    }

//    public class EmailSender : ISenderEmail
//    {
//        private readonly IConfiguration _configuration;

//        public EmailSender(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public Task SendEmailAsync(string ToEmail, string Subject, string Body, bool IsBodyHtml = false)
//        {
//            string MailServer = _configuration["EmailSettings:MailServer"];
//            string FromEmail = _configuration["EmailSettings:FromEmail"];
//            string Password = _configuration["EmailSettings:Password"];
//            int MailPort = int.Parse(_configuration["EmailSettings:MailPort"]);


//            var client = new SmtpClient(MailServer, MailPort)
//            {
//                Credentials = new NetworkCredential(FromEmail, Password),
//                EnableSsl = true,
//            };

//            MailMessage mailMessage = new MailMessage(FromEmail, ToEmail, Subject, Body)
//            {
//                IsBodyHtml = IsBodyHtml
//            };

//            return client.SendMailAsync(mailMessage);
//        }
//    }
//}