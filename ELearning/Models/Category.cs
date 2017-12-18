using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required,Display(Name ="Main Category")]
        public string Name { get; set; }
        public string Discription { get; set; }

       
        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}