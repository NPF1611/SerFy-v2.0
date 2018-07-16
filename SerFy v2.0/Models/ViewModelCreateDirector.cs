using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelCreateDirector
    {
        [Required(ErrorMessage ="Name is required")]
        //Director Name
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
           ErrorMessage = "The {0} only accepts letters and blank spaces.Each word begins in upper case, followed by lower case...")]
        public string Name { get; set; }
        
        //Director Photograph
        public string Photograph { get; set; }
        
        // Director BIRTH DATE
        [Display(Name="Birth date")]
        public DateTime Place_BD { set; get; }
        
        //Director Biography
        [Required(ErrorMessage ="Biography is required")]
        [Display(Name="Biography")]
        public string MiniBio { get; set; }
        
        //Directors Movies
        [ForeignKey("Movie")]
        [Display(Name = "Movies")]
        public int[] MovieFK { get; set; }
        public virtual ICollection<Movie> MovieList { get; set; }

        //Databasee Movies
        [ForeignKey("Movie")]
        [Display(Name="Movie")]
        public int[] MovieAllFK { get; set; }
        public virtual ICollection<Movie> MovieAllList { get; set; }

    }
}


