namespace Developer.NotificationSender
{
    public class NotificationFactory
    {
        static public INotificationSender SelectChannel(NotificationType channel)
        {
            INotificationSender objSelected = null;
            switch (channel)
            {
                case NotificationType.Email:
                    objSelected = new EmailSender();
                    break;
                default:
                    objSelected = new EmailSender();
                    break;
            }
            return objSelected;
        }
    }
}
