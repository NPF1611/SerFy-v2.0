using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}