using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelEditActors
    {
        
        //The Actor Name
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        //The photograph name
        public string Photograph { get; set; }
        // The date and place where the Actor was born
        [Display(Name="Birth Date")]
        public DateTime BD { get; internal set; }
        //a small Biography 

        [Required(ErrorMessage ="Biography is Required")]
        [Display(Name="Biography")]
        public string Minibio { get; set; }

        //value IdValue
        public int IdValue { get; set; }
        
        
        //Characters List and ids Array
        [ForeignKey("Character")]
        [Display(Name="Characters")]
        public int[] IdsCha { get; set; }
        public virtual IEnumerable<Characters> ListCha { get; set; }



        //Characters List and ids Array
        [Display(Name = "Characters")]
        [ForeignKey("Character")]
        public int[] IdsAllCha { get; set; }
        public virtual IEnumerable<Characters> ListAllCha { get; set; }
    }
}