public class ObjectFactory<T>
{
   // public static T Get(String Key)
    public static T Get()
    {
        //if(Key.Equals("notification-Service",StringComparison.CurrentCultureIgnoreCase))
        if(typeof(T)==typeof(NotificationService))
        {
           var emailService=ObjectFactory<IEmailService>.Get();
           return (T)(object)new NotificationService(emailService);  // Generic type er jonno amra object e cast kore return korbo
        }
      // if(Key.Equals("email-Service",StringComparison.CurrentCultureIgnoreCase))
       if(typeof(T)==typeof(EmailService))
        {
           return (T)(object)new EmailService();
        }
        throw new ArgumentException($"Invalid Key: {typeof(T)}");
       
    }
}