using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailFunc
{
    public class EmailProcessor : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly SmtpConfiguration _configuration;
        public EmailProcessor(ILogger<EmailProcessor> logger, IOptions<SmtpConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;
        }

        public void SendEmail(string recever, string messagebody, string subject)
        {
            _logger.LogInformation($"Sending mail to the {recever}");
            try
            {
                string sender = _configuration.Sender;
                MailMessage message = new MailMessage(sender,recever);
                message.IsBodyHtml = true;
                message.Body = messagebody;
                message.Subject = subject;
                var attachment = new System.Net.Mail.Attachment(_configuration.Attachment);
                message.Attachments.Add(attachment);
                using(var smtpClient = new SmtpClient(_configuration.SmtpServer, _configuration.Port))
                {
                    smtpClient.EnableSsl = _configuration.EnableSsl;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_configuration.Username,_configuration.Password);
                    smtpClient.EnableSsl = _configuration.EnableSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(message);
                }
                _logger.LogInformation($"Mail message to the {recever} sent!!!");

            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured during Sending mail to the {recever}. Error Message: {e.Message}");

                throw;
            }
        }
    }
}
