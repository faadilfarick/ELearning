using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class SubCategory
    {
        public int ID { get; set; }
        [Required,Display(Name ="Sub-Category Name")]
        public string Name { get; set; }
        public string Discription { get; set; }

        public virtual Category Category { get; set; }
    }
}