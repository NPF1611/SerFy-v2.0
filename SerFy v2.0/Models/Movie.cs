using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SerFy_v2._0.Models
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Photograph { get; set; }
        [Required]
        public string Trailer { get; set; }
        [Required]
        [Display(Name = "Information")]
        public string sinopse { get; set; }
        public DateTime? dataDePub { get; set; }
        public double Rating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Characters> CharactersList { get; set; }
        public virtual ICollection<Director> DirectorList { get; set; }
        public virtual ICollection<Writer> WriterList { get; set; }
    }
}