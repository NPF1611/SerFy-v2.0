using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelCreateActors
    {
        
        //The Actor Name
        [Required(ErrorMessage ="Name is required")]

        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
           ErrorMessage = "The {0} only accepts letters and blank spaces.Each word begins in upper case, followed by lower case...")]
        public string Name { get; set; }
        //The photograph name
        public string Photograph { get; set; }
        
        // The date and place where the Actor was born
        [Display(Name="Birth Date")]
        public DateTime BD { get; internal set; }

        //a small Biography 
        [Display(Name="Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Minibio { get; set; }

        //Characters List and ids 
        [ForeignKey("Characters")]
        [Display(Name ="Characters")]
        public int[] IdsCha { get; set; }
        public virtual IEnumerable<Characters> ListCha { get; set; }

        //All the Characters Lis and ids
        [ForeignKey("Character")]
        public int[] IdsAllCha { get; set; }
        public virtual IEnumerable<Characters> ListAllCha { get; set; }
    }
}