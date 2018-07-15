using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Director
    {
        [Key]
        //Id value
        public int ID { get; set; }
        
        //Director Name
        public string Name { get; set; }
        
        //Director Photograph
        public string Photograph { get; set; }
        
        //Director Birth Date
        [Display(Name="Birth Date")]
        public DateTime Place_BD { set; get; }
        
        //Director Biography
        [Display(Name="Biography")]
        public string MiniBio { get; set; }
        
        //Director Movies
        [Display(Name="Movies")]
        public virtual ICollection<Movie> MovieList { get; set; }
    }
}