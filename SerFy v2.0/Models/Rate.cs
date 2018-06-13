using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Rate
    {
        public int ID { get; set; }
        public int rate { get; set; }
        public Movie Movie { get; set; }

        //public user User { get; set; }
    }
}