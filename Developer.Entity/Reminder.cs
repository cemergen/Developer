using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Entity
{
    public class Reminder : BaseEntity
    {
        [Display(Name = "Reminder Name")]
        [Required(ErrorMessage = "Reminder Name is required.")]
        [MaxLength(40, ErrorMessage = "Reminder Name cannot be longer than 40 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Reminder Date is required.")]
        public DateTime RemindDate { get; set; }
        [Required(ErrorMessage = "Reminder Note is required.")]
        [MaxLength(40, ErrorMessage = "Reminder Name cannot be longer than 40 characters.")]
        public string ReminderNote { get; set; }
        public bool IsCompleted { get; set; }
        public virtual TaskItem TaskItem { get; set; }
    }
}
