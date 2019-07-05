using Predica.CommonServices.ConfiguratorManager;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Sendgrid
{
    public class EmailService : IEmailService
    {
        private string _sendgridKey;
        private string _sendgridAdminEmail;
        private IConfigurationManagerHelper _configurationManagerHelper;
        public EmailService(IConfigurationManagerHelper config)
        {
            _configurationManagerHelper = config;
            _sendgridKey = _configurationManagerHelper.Sendgrid_Key;
            _sendgridAdminEmail = _configurationManagerHelper.Sendgrid_AdminMail;
        }
        private async Task SendEmailTemplate<T>(List<Personalization<T>> personalization, string templateId)
        {
            var per = new SendgridEmailTemplateSend<T>()
            {
                From = new From()
                {
                    Email = _sendgridAdminEmail,
                    Name = "Test",
                },
                Template_id = templateId,
                Personalizations = personalization,
            };
            var client = new SendGridClient(_sendgridKey);
            var response = await client.RequestAsync(SendGridClient.Method.POST,
                                              Newtonsoft.Json.JsonConvert.SerializeObject(per), urlPath: "mail/send");
        }
        private async Task CreateMailAsync(string toEmail, string fromEmail, string mailName, string subject, string plainText, string content)
        {
            var apiKey = _sendgridKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, mailName);
            var to = new EmailAddress(toEmail);
            //    var plainTextContent = Regex.Replace("sadasd", "<[^>]*>", "");
            var plainTextContent = plainText;
            var htmlContent = content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        public async Task SendEmailasync(string toEmail, string body)
        {
            await CreateMailAsync(toEmail, _sendgridAdminEmail, "Interview", "Interview", "Interview", body);
        }

    }
}
