using System;
using System.Net;
using System.Web.Mvc;
using Developer.Data.Repo;
using Developer.Entity;
using log4net;

namespace Developer.Web.Controllers
{
    public class RemindersController : Controller
    {
        private UnitOfWork unitOfWork;
        private Repository<Reminder> reminderRepo;
        private Repository<TaskItem> taskItemRepo;


        public RemindersController()
        {
            unitOfWork = new UnitOfWork();
            reminderRepo = unitOfWork.Repository<Reminder>();
            taskItemRepo = unitOfWork.Repository<TaskItem>();

        }

        // GET: Reminders
        public ActionResult Index()
        {
            return View(reminderRepo.Table);
        }

        // GET: Reminders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = reminderRepo.GetbyId(id.Value);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // GET: Reminders/Create
        public ActionResult Create(int TaskItemId)
        {
            ViewBag.TaskItemId = TaskItemId;
            return View();
        }

        // POST: Reminders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RemindDate,ReminderNote")] Reminder reminder , int TaskItemId)
        {
            if (ModelState.IsValid)
            {
                var dateTimeNow = DateTime.Now;
                var newReminder = new Reminder
                {
                    IsCompleted = false,
                    ModifiedDate = dateTimeNow,
                    CreateDate = dateTimeNow,
                    IsDeleted = false,
                    RemindDate = reminder.RemindDate,
                    TaskItem = taskItemRepo.GetbyId(TaskItemId),
                    ReminderNote = reminder.ReminderNote,
                    Name = reminder.Name
                };
                reminderRepo.Insert(newReminder);
                return RedirectToAction("Details", "TaskItems",new { Id = TaskItemId });
            }

            return View(reminder);
        }

        // GET: Reminders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = reminderRepo.GetbyId(id.Value);

            if (reminder == null)
            {
                return HttpNotFound();
            }
            return View(reminder);
        }

        // POST: Reminders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RemindDate,ReminderNote,IsCompleted,CreateDate,ModifiedDate,IsDeleted")] Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                reminderRepo.Update(reminder);
                return RedirectToAction("Index");
            }
            return View(reminder);
        }

        // GET: Reminders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reminder reminder = reminderRepo.GetbyId(id.Value);
            if (reminder == null)
            {
                return HttpNotFound();
            }
            reminder.ModifiedDate = DateTime.Now;
            reminder.IsDeleted = true;
            reminderRepo.Update(reminder);
            return Json(reminder.Name, JsonRequestBehavior.AllowGet);
        }

      

       
    }
}
