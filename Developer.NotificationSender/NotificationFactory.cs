namespace Developer.NotificationSender
{
    public class NotificationFactory
    {
        static public INotificationSender SelectChannel(string channel)
        {
            INotificationSender objSelected = null;
            if (channel == "email")
            {
                objSelected = new EmailSender();
            }
            return objSelected;
        }
    }
}
