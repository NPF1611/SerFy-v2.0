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

        [Required]
        public string Name { get; set; }
        [Required]
        public string Photograph { get; set; }

        [ForeignKey("Movie")]
        public int MovieFK { get; set; }
        public virtual ICollection<Movie> MoviesList { get; set; }

        [ForeignKey("actor")]
        public int actorFK { get; set; }
        public virtual Actors actor { get; set; }
    }
}