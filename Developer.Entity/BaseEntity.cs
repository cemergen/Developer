using System;
using System.ComponentModel.DataAnnotations;

namespace Developer.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
