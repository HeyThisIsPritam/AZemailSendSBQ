namespace SendEmailFunc
{
    public class SmtpConfiguration
    {
        public string Username { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Attachment { get; set; }  

    }
}