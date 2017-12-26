using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Models
{
    public class ForumPostReply
    {
        public int ID { get; set; }
        public string Answer { get; set; }

        public virtual ForumPost ForumPost { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}