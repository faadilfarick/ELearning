using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class Course
    {
        

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Videos> Videos { get; set; }
    }
}