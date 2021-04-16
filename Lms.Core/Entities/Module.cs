using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lms.Core.Entities
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
    }
}
