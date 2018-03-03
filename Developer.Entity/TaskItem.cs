using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Entity
{
    public class TaskItem : BaseEntity
    {
        public TaskItem()
        {
            Reminders = new List<Reminder>();
        }
        [Display(Name = "Task Name")]
        [Required(ErrorMessage = "Task Name is required.")]
        [MaxLength(40, ErrorMessage = "Task Name cannot be longer than 40 characters.")]
        public string Name { get; set; }
        [Display(Name = "Task Description")]
        [Required(ErrorMessage = "Task Description is required.")]
        [MaxLength(40, ErrorMessage = "Task Description cannot be longer than 40 characters.")]
        public string Description { get; set; }
        public virtual ToDoItem ToDoItem { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
