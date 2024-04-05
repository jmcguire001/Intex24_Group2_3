using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Intex24_Group2_3.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Fetch email settings from Azure Key Vault
        var secretClient = new SecretClient(new Uri("https://intex.vault.azure.net/"), new DefaultAzureCredential());

        // Replace "EmailSettings" with the name of your secret containing email settings
        KeyVaultSecret mailServerSecret = await secretClient.GetSecretAsync("EmailSettings");

        // Deserialize secret value to EmailSettings object
        var emailSettings = JsonConvert.DeserializeObject<EmailSettings>(mailServerSecret.Value);

        // Configure and send the email using SMTP
        var client = new SmtpClient(emailSettings.MailServer)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailSettings.FromEmail, emailSettings.Password),
            EnableSsl = true,
            Port = emailSettings.MailPort
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailSettings.FromEmail, emailSettings.SenderName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}