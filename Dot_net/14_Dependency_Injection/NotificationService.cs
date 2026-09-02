// DIP--> higher level module lower level module er upor depend korbe na
// hitg level module
public class NotificationService(IEmailService emailService) // this is primary constructor
{
    public void NotifyUser(string userEmail, string message)
    {
        emailService.SendEmail(userEmail, "Notification", message);
    }
}
public interface IEmailService
{
    void SendEmail(string to,string userEmail, string message);
}
// lower evel module
public class EmailService:IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"sending email to {to}");
    }
}