using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.Core.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
