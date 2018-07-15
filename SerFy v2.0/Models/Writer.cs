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
        [Required]
        public string Name { get; set; }
        public string Photograph { get; set; }
        public DateTime Place_DB { get; set; }
        public string MiniBio { get; set; }
        public virtual ICollection<Movie> MoviesList { get; set; }
    }
}