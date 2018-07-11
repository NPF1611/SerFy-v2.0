using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Actors
    {
        public int ID { get; set; }
        //The Actor Name
        [Required]
        public string Name { get; set; }
        //The photograph name
        public string Photograph { get; set; }
        // The date and place where the Actor was born
        public DateTime BD { get;  set; }
        //a small Biography 
        public string Minibio { get; set; }

        public virtual ICollection<Characters> CharacterList { get; set; }
    }
}