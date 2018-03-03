using Developer.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Data
{
    public class DevContext : DbContext
    {
        public DevContext() : base("DevContextConnectionString")
        {

        }

        public virtual DbSet<ToDoItem> ToDoItems { get; set; }
        public virtual DbSet<TaskItem> TaskItems { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
    }
}
