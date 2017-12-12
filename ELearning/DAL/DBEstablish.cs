using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ELearning.DAL
{
    public class DBEstablish
    {
        public static string makeConnection()
        {
            string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            return con;
        }
    }
}