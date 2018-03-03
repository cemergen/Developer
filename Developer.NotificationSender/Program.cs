using Developer.Data.Repo;
using Developer.Entity;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Developer.NotificationSender
{
    class Program
    {
        private UnitOfWork unitOfWork;
        private Repository<Reminder> repo;
        private static readonly ILog Log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();
        }

        private void Start()
        {
            Log.Info("NotificationSender Started");
            var toSendData = GetReadyData();

            INotificationSender objSender;
            objSender = NotificationFactory.SelectChannel("email");
            Email(objSender, toSendData);
        }

        private void Email(INotificationSender objSender, List<Reminder> toSendData)
        {
            foreach (var item in toSendData)
            {
                try
                {
                    objSender.Send(item);
                    MarkAsSent(item);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        private List<Reminder> GetReadyData()
        {
            unitOfWork = new UnitOfWork();
            repo = unitOfWork.Repository<Reminder>();
            var DateTodayStart = DateTime.Now.Date;
            var DateTodayEnd = DateTodayStart.AddDays(1).AddSeconds(-1);
            List<Reminder> data = repo.Table.AsQueryable().Where(n => !n.IsCompleted && n.RemindDate <= DateTodayEnd && n.RemindDate >= DateTodayStart).OrderBy(n => n.RemindDate).ToList();
            return data;
        }

        private void MarkAsSent(Reminder reminder)
        {
            reminder.IsCompleted = true;
            repo.Update(reminder);
        }

    }
}

