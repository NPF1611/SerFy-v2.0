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
        //"User" ID
        [Key]
        public int ID { get; set; }
        //Displayed as UserName
        [Display(Name="UserName")]
        [Required(ErrorMessage ="UserName is Required")]
        public string UName { get; set; }
        //User Name
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        //User email
        public string email { get; set; }
        //User icon
        public string photo { get; set; }
        //day and hours when the user created the account
        public DateTime CRTime { get; set; }

        //User Comments
        public virtual ICollection<Comment> Comments { get; set; }

        //User Rate 
        public virtual ICollection<Rate> Rate { get; set; }
    }
}