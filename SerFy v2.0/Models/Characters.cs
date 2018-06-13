using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Characters
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photograph { get; set; }
        public virtual ICollection<Movie> MoviesList { get; set; }

        public virtual Actors actor { get; set; }
    }

}