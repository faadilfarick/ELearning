using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class PurchasedCourse
    {
        public int ID { get; set; }
        public virtual Course Course { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}