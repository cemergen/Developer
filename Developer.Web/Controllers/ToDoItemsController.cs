using System;
using System.Net;
using System.Web.Mvc;
using Developer.Data.Repo;
using Developer.Entity;

namespace Developer.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        private UnitOfWork unitOfWork;
        private Repository<ToDoItem> todoItemRepo;
        private Repository<TaskItem> taskItemRepo;
        private Repository<Reminder> reminderRepo;

        public ToDoItemsController()
        {
            unitOfWork = new UnitOfWork();
            todoItemRepo = unitOfWork.Repository<ToDoItem>();
            taskItemRepo = unitOfWork.Repository<TaskItem>();
            reminderRepo = unitOfWork.Repository<Reminder>();
        }


        // GET: ToDoItems
        public ActionResult Index()
        {
            return View(todoItemRepo.Table.FindAll(m=>!m.IsDeleted));
        }

        // GET: ToDoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var toDoItem = todoItemRepo.GetbyId(id.Value);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaskItems = toDoItem.TaskItems;
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Name")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                var DateTimeNow = DateTime.Now;
                toDoItem.CreateDate = DateTimeNow;
                toDoItem.ModifiedDate = DateTimeNow;
                toDoItem.IsDeleted = false;
                todoItemRepo.Insert(toDoItem);
                return RedirectToAction("Index");
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = todoItemRepo.GetbyId(id.Value);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Name")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                var updateRecord = todoItemRepo.GetbyId(toDoItem.Id);
                updateRecord.ModifiedDate = DateTime.Now;
                updateRecord.Description = toDoItem.Description;
                updateRecord.Name = toDoItem.Name;
                todoItemRepo.Update(updateRecord);
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            var DateTimeNow = DateTime.Now;
            var toDelete = todoItemRepo.GetbyId(id.Value);

            toDelete.IsDeleted = true;
            toDelete.ModifiedDate = DateTimeNow;
            todoItemRepo.Update(toDelete);

            foreach (var tskItem in toDelete.TaskItems)
            {
                tskItem.IsDeleted = true;
                tskItem.ModifiedDate = DateTimeNow;
                taskItemRepo.Update(tskItem);
                foreach (var rmdItem in tskItem.Reminders)
                {
                    rmdItem.IsDeleted = true;
                    rmdItem.ModifiedDate = DateTimeNow;
                    reminderRepo.Update(rmdItem);
                }
            }

            return Json(toDelete.Description,JsonRequestBehavior.AllowGet);
        }
       
    }
}
