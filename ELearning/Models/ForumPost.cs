using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class ForumPost
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Discription { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}