using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelEditWriter
    {
        //id value
        public int IDValue { get; set; }
        [Required(ErrorMessage ="Name is required")]
        //Name
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
           ErrorMessage = "The {0} only accepts letters and blank spaces.Each word begins in upper case, followed by lower case...")]
        public string Name { get; set; }
        //Photograph
        public string Photograph { get; set; }
        //Birth Date
        public DateTime Place_BD { set; get; }
        //Biography
        [Required(ErrorMessage = "Biography is required")]
        [Display(Name="Biography")]
        public string MiniBio { get; set; }
        //Movies
        [ForeignKey("Movie")]
        [Display(Name="Movie")]
        public int[] MovieFK { get; set; }
        public virtual ICollection<Movie> MovieList { get; set; }
        //Movies
        [ForeignKey("Movie")]
        [Display(Name = "Movie")]
        public int[] MovieAllFK { get; set; }
        public virtual ICollection<Movie> MovieAllList { get; set; }

    }
}


