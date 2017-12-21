using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace ELearning.Models
{
   
    public class Forum
    {
        public int ID { get; set; }
        public string CourseTitle { get; set; }
        public string Description { get; set; }

         public virtual Course Course { get; set; }
    }
}