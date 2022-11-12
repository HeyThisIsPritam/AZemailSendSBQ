namespace SendEmailFunc
{
    public interface IEmailSender
    {
        void SendEmail(string recever, string messagebody,string subject);
    }
}