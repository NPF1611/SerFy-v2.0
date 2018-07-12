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
        [Required]
        public string Name { get; set; }
        public string Photograph { get; set; }
        public DateTime Place_BD { set; get; }
        public string MiniBio { get; set; }

        [ForeignKey("Movie")]
        public int[] MovieFK { get; set; }
        public virtual ICollection<Movie> MovieList { get; set; }

        [ForeignKey("Movie")]
        public int[] MovieAllFK { get; set; }
        public virtual ICollection<Movie> MovieAllList { get; set; }

    }
}


