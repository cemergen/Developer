using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Entity
{
    public class ToDoItem : BaseEntity
    {
        public ToDoItem()
        {
            TaskItems = new List<TaskItem>();
        }
        [Required(ErrorMessage = "Todo Item Name is required.")]
        [Display(Name = "ToDo Item Name")]
        [MaxLength(40, ErrorMessage = "ToDo Item Name cannot be longer than 40 characters.")]
        public string Name { get; set; }

        [Display(Name = "Todo Item Description")]
        [Required(ErrorMessage = "Todo Item Description is required.")]
        [MaxLength(40, ErrorMessage = "Todo Item Description cannot be longer than 40 characters.")]
        public string Description { get; set; }
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
