using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelCreateCharacter
    {
        //Character Name
        [Required]
        public string Name { get; set; }
        //Character Photo
        [Required]
        public string Photograph { get; set; }
        //Moviess that the character is into 
        [ForeignKey("Movie")]
        public int MovieFK { get; set; }
        public virtual ICollection<Movie> MoviesList { get; set; }
        //Actor that plays the role
        [ForeignKey("actor")]
        public int actorFK { get; set; }
        public virtual Actors actor { get; set; }
    }
}