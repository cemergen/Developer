using Developer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.NotificationSender
{
    public interface INotificationSender
    {
        void Send(Reminder reminder);
    }
}
