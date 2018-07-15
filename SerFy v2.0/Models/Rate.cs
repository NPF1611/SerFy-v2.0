using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Rate
    {
        //Rate ID
        public int ID { get; set; }
        //rate value
        public int rate { get; set; }
        
        //Movie being Rate 
        [ForeignKey("Movie")]
        public int MovieFK { get; set; }
        public Movie Movie { get; set; }

        //user that rates
        [ForeignKey("User")]
        public int UserFK { get; set; }
        public virtual User User { get; set; }
    }
}