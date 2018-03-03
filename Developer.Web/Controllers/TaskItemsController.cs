using System;
using System.Net;
using System.Web.Mvc;
using Developer.Data.Repo;
using Developer.Entity;

namespace Developer.Web.Controllers
{
    public class TaskItemsController : Controller
    {
        private UnitOfWork unitOfWork;
        private Repository<TaskItem> taskItemRepo;
        private Repository<ToDoItem> todoItemRepo;
        private Repository<Reminder> reminderRepo;
        public TaskItemsController()
        {
            unitOfWork = new UnitOfWork();
            taskItemRepo = unitOfWork.Repository<TaskItem>();
            todoItemRepo = unitOfWork.Repository<ToDoItem>();
            reminderRepo = unitOfWork.Repository<Reminder>();
        }


        // GET: TaskItems
        public ActionResult Index()
        {
            return View(taskItemRepo.Table);
        }

        // GET: TaskItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = taskItemRepo.GetbyId(id.Value);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            return View(taskItem);
        }

        // GET: TaskItems/Create
        public ActionResult Create(int TodoItemId)
        {
            var todoItem = todoItemRepo.GetbyId(TodoItemId);
            ViewBag.TodoItemDescription = todoItem.Description;
            ViewBag.TodoItemId = TodoItemId;
            return View();
        }

        // POST: TaskItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,Name")] TaskItem taskItem,int TodoItemId)
        {
            if (ModelState.IsValid)
            {
                var DateTimeNow = DateTime.Now;
                var taskItemNew = new TaskItem
                {
                    CreateDate = DateTimeNow,
                    ModifiedDate = DateTimeNow,
                    Description = taskItem.Description,
                    Name =taskItem.Name,
                    IsDeleted = false,
                    ToDoItem = todoItemRepo.GetbyId(TodoItemId)
                };
                
                taskItemRepo.Insert(taskItemNew);
                return RedirectToAction("Details","ToDoItems",new { id= TodoItemId });
            }

            return View(taskItem);
        }

        // GET: TaskItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskItem taskItem = taskItemRepo.GetbyId(id.Value);
            if (taskItem == null)
            {
                return HttpNotFound();
            }
            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Name")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                var update = taskItemRepo.GetbyId(taskItem.Id);
                update.ModifiedDate = DateTime.Now;
                update.Description = taskItem.Description;
                update.Name = taskItem.Name;
                taskItemRepo.Update(update);
                return RedirectToAction("Details", "ToDoItems", new { id = update.ToDoItem.Id });
            }

            return View(taskItem);
        }

        // GET: TaskItems/Delete/5
        public ActionResult Delete(int? id)
        {
            var DateTimeNow = DateTime.Now;
            var tskItem = taskItemRepo.GetbyId(id.Value);

            tskItem.IsDeleted = true;
            tskItem.ModifiedDate = DateTimeNow;
            taskItemRepo.Update(tskItem);
          
                foreach (var rmdItem in tskItem.Reminders)
                {
                    rmdItem.IsDeleted = true;
                    rmdItem.ModifiedDate = DateTimeNow;
                    reminderRepo.Update(rmdItem);
                }

            return Json(tskItem.Description, JsonRequestBehavior.AllowGet);
        }

        
    }
}
