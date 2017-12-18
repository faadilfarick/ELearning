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
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please Enter Numbers Only")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Category MainCategory { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Videos> Videos { get; set; }
       // public virtual ICollection<Category> CategoryList { get; set; }
    }
}