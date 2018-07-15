using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelEditDirector
    {
        //Director ID
        public int IDValue { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        //Photograph
        public string Photograph { get; set; }
        //Director Birth Date
        [Display(Name="Birth Date")]
        public DateTime Place_BD { set; get; }
        //Director Biography
        [Display(Name="Biography")]
        [Required(ErrorMessage ="Biography is required")]
        public string MiniBio { get; set; }
        
        //Director Movies
        [ForeignKey("Movie")]
        [Display(Name="Movies")]
        public int[] MovieFK { get; set; }
        public virtual ICollection<Movie> MovieList { get; set; }
        
        //All Movies
        [ForeignKey("Movie")]
        [Display(Name="Movies")]
        public int[] MovieAllFK { get; set; }
        public virtual IEnumerable<Movie> MovieAllList { get; set; }

    }
}


