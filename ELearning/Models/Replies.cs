using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class Replies
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string Description { get; set; }
    }
}