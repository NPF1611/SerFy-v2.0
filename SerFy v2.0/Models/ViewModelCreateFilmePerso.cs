using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelCreateFilmePerso
    {
        //Create Movies Model

        //Movie Title
        [Required(ErrorMessage ="The Movie Title it is required ")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        //Movie cover 
        [Display(Name = "Cover")]
        public string Photograph { get; set; }

        //Movie Trailer
        [Required(ErrorMessage = "The Movie Trailer it is required ")]
        [Display(Name = "Trailer")]
        public string Trailer { get; set; }

        //Movie Information 
        [Required(ErrorMessage ="The Movie synopses it is required ")]
        [Display(Name = "synopsis")]
        public string sinopse { get; set; }

        //Movie Publication Date 
        [Display(Name = "Publication Date:")]
        public DateTime dataDePub { get; set; }

        //Movie Rate
        [Display(Name ="Rate")]
        public double Rating { get; set; }

        //Movie Characters List
        [ForeignKey("Characters:")]
        public int[] idsCharacters { get; set; }
        public IEnumerable<Characters> Listcharacters { get; set; }

        //Movie DirectorsList
        [ForeignKey("Directors:")]
        public int[] idsDirectores { get; set; }
        public IEnumerable<Director> ListDirectors { get; set; }

        //Movie Writers List
        [ForeignKey("Writers:")]
        public int[] idsWriters { get; set; }
        public IEnumerable<Writer> ListWriters { get; internal set; }
    }
}


