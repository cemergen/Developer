using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Developer.Entity;

namespace Developer.NotificationSender
{
    public class EmailSender : INotificationSender
    {
        public void Send(Reminder reminder)
        {
            SmtpClient client = new SmtpClient();
            client.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = ConfigurationManager.AppSettings["Host"];
            var mail = new MailMessage(ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["To"]);
            mail.Subject = ConfigurationManager.AppSettings["Subject"];
            mail.Body = string.Format("Reminder Name :{0} Reminder Note :{1} Reminder Task :{2} Reminder To-Do :{3}", reminder.Name, reminder.ReminderNote,reminder.TaskItem.Name,reminder.TaskItem.ToDoItem.Name);
            client.Send(mail);
        }
    }
}

   

