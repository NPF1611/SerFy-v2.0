using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Director
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photograph { get; set; }
        public DateTime Place_BD { set; get; }
        public string MiniBio { get; set; }
        public virtual ICollection<Movie> MovieList { get; set; }
    }
}