namespace Service.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string receiver, string subject, string content);
    }
}
