using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="UserName")]
        public string UName { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public DateTime CRTime { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rate> Rate { get; set; }
    }
}