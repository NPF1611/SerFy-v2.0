using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class viewModelEditFilme
    {



        public int IDvalue { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Photograph { get; set; }
        [Required]
        public string Trailer { get; set; }
        [Required]
        [Display(Name = "Information")]
        public string sinopse { get; set; }
        public DateTime dataDePub { get; set; }
        public double Rating { get; set; }

        [Display(Name = "Characters")]
        public int[] idsCharacters { get; set; }

        public IEnumerable<Characters> Listcharacters { get; set; }
        [Display(Name = "Directors")]
        public int[] idsDirectores { get; set; }

        public IEnumerable<Director> ListDirectors { get; set; }

        [Display(Name = "Writers")]
        public int[] idsWriters { get; set; }
        public IEnumerable<Writer> ListWriters { get; internal set; }

        [Display(Name = "Comments")]
        public int[] idsComments { get; set; }
        public IEnumerable<Comment> ListComments { get; internal set; }

        [Display(Name = "Rate")]
        public int[] idsRates { get; set; }
        public IEnumerable<Rate> ListRates { get; internal set; }

        //param w/the total 

        [Display(Name = "Characters")]
        public int[] idsAllCharacters { get; set; }

        public IEnumerable<Characters> ListAllcharacters { get; set; }

        [Display(Name = "Directors")]  
        public int[] idsAllDirectores { get; set; }

        public IEnumerable<Director> ListAllDirectors { get; set; }

        [Display(Name = "Writers")]
        public int[] idsAllWriters { get; set; }
        public IEnumerable<Writer> ListAllWriters { get; internal set; }

        [Display(Name = "Comments")]
        public int[] idsAllComments { get; set; }
        public IEnumerable<Comment> ListAllComments { get; internal set; }

        [Display(Name = "Rate")]
        public int[] idsAllRates { get; set; }
        public IEnumerable<Rate> ListAllRates { get; internal set; }


    }
}