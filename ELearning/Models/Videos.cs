using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class Videos
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }

        [Display(Name ="File Path")]
        public string  FilePath { get; set; }

        //public IEnumerable<HttpPostedFileBase> file { get; set; }

        //public IEnumerable<HttpPostedFileBase> file { get; set; }
        public virtual Course Course { get; set; }
    }
}