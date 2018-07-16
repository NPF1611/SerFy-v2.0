using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class Actors
    {
        //the actor id
        public int ID { get; set; }
        //The Actor Name
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        //The photograph name
        public string Photograph { get; set; }
        // The date and place where the Actor was born
        [Display(Name = "Birth Date")]
        public DateTime BD { get; set; }
        //a small Biography 
        [Required(ErrorMessage = "Biography is required")]
        [Display(Name = "Biography")]
        public string Minibio { get; set; }

        public virtual ICollection<Characters> CharacterList { get; set; }
    }
}