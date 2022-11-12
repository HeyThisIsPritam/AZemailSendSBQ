using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SendEmailFunc
{
    public class EmailSend
    {
        private readonly IEmailSender _emailSender;
        public EmailSend(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [FunctionName("EmailSend")]
        public void Run([ServiceBusTrigger("messageq",Connection = "emailconnection")]string messageBody,ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue Trigger processed message: {messageBody}");
            var emailMsg = JsonConvert.DeserializeObject<EmailServiceBusMessage>(messageBody);
            _emailSender.SendEmail(emailMsg.ReceverMail, emailMsg.MessageBody, emailMsg.Subject);

        }
    }
}
