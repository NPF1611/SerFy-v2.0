using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Writer
    {
        [Key]
        //Id value
        public int ID { get; set; }
        //Name
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        //Photograph
        public string Photograph { get; set; }
        //Writer Birth Date
        public DateTime Place_DB { get; set; }
        //Biography
        [Required(ErrorMessage = "Biography is required")]
        [Display(Name= "Biography")]
        public string MiniBio { get; set; }
        //Movies List
        [Display(Name="Movies")]
        public virtual ICollection<Movie> MoviesList { get; set; }
    }
}