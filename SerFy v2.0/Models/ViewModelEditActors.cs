using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelEditActors
    {
        
        //The Actor Name
        [Required]
        public string Name { get; set; }
        //The photograph name
        public string Photograph { get; set; }
        // The date and place where the Actor was born
        public DateTime BD { get; internal set; }
        //a small Biography 
        public string Minibio { get; set; }
        //value IdValue
        public int IdValue { get; set; }
        
        
        //teste
        [Display(Name = "Character")]
        public int[] IdsCha { get; set; }
        public virtual IEnumerable<Characters> ListCha { get; set; }


        //fks
        [Display(Name = "Character")]
        public int[] IdsAllCha { get; set; }
        public virtual IEnumerable<Characters> ListAllCha { get; set; }
    }
}