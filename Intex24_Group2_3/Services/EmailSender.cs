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
    public readonly EmailSettings _emailSettings;

    public EmailSender(IConfiguration configuration)
    {
        _emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
    }
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
        // var secretClient = new SecretClient(new Uri("https://intex.vault.azure.net/"), new DefaultAzureCredential()); // Uncomment this to access key vault
        // Replace "EmailSettings" with the name of your secret containing email settings
        // KeyVaultSecret mailServerSecret = await secretClient.GetSecretAsync("EmailSettings"); // Uncomment this to access key vault
        // Deserialize secret value to EmailSettings object
        // var emailSettings = JsonConvert.DeserializeObject<EmailSettings>(mailServerSecret.Value); // Uncomment this to access key vault

        // Configure and send the email using SMTP
        var client = new SmtpClient(_emailSettings.MailServer)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.Password),
            EnableSsl = true,
            Port = _emailSettings.MailPort
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.FromEmail, _emailSettings.SenderName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}