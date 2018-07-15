using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Movie
    {
        //id value
        [Key]
        public int ID { get; set; }
        //Movie Title
        [Required]
        [Display(Name = "Title")]
        //Movie Name
        public string Name { get; set; }
        //Movie Cover
        public string Photograph { get; set; }
        
        //Movie Trailer
        [Required]
        public string Trailer { get; set; }

        //Movie Synopses
        [Required]
        [Display(Name = "Information")]
        public string sinopse { get; set; }

        //Movie Publication Date 
        public DateTime dataDePub { get; set; }

        //Movie Rating 
        public double Rating { get; set; }

        //Movie Comments List
        public virtual ICollection<Comment> Comments { get; set; }

        //Movie Rates List
        public virtual ICollection<Rate> Rates { get; set; }

        //Movie Characters List
        public virtual ICollection<Characters> CharactersList { get; set; }
        
        //Movie Directors List
        public virtual ICollection<Director> DirectorList { get; set; }
        
        //Movie Writers List
        public virtual ICollection<Writer> WriterList { get; set; }
    }
}