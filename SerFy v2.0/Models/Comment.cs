using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Comment
    {
        //comment ID
        [key]
        public int ID { get; set; }
        //Comment itself
        public string Text { get; set; }
        //Movie
        [ForeignKey("Movie")]
        public int MovieFK { get; set; }
        public virtual Movie Movie { get; set; }
        //User
        [ForeignKey("User")]
        public int UserFK { get; set; }
        public virtual User User { get; set; }
    }
}