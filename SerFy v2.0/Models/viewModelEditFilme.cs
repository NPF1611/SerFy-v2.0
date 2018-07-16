using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class viewModelEditFilme
    {


        //Movie ID value
        public int IDvalue { get; set; }

        //Movie Title
        [Required(ErrorMessage = "The Movie Title it is required ")]
        [Display(Name = "Title")]
        public string Name { get; set; }

        //Movie cover 
        [Display(Name ="Cover")]
        public string Photograph { get; set; }

        //Movie Trailer
        [Required(ErrorMessage = "The Movie Trailer it is required ")]
        [Display(Name = "Trailer")]
        public string Trailer { get; set; }
        
        //Movie Information 
        [Required(ErrorMessage = "The Movie synopses it is required ")]
        [Display(Name = "synopsis")]
        public string sinopse { get; set; }

        //Movie Publication Date 
        [Display(Name = "Publication Date:")]
        public DateTime dataDePub { get; set; }

        //Movie Rate
        [Display(Name = "Rate")]
        public double Rating { get; set; }

        //Movie Characters List
        [ForeignKey("Characters")]
        [Display(Name = "Characters")]
        public int[] idsCharacters { get; set; }
        [Display(Name = "Characters")]
        public IEnumerable<Characters> Listcharacters { get; set; }

        //Movie DirectorsList
        [ForeignKey("Directors:")]
        [Display(Name = "Directors")]
        public int[] idsDirectores { get; set; }
        [Display(Name = "Directors")]
        public IEnumerable<Director> ListDirectors { get; set; }

        //Movie Writers List
        [ForeignKey("Writers:")]
        [Display(Name = "Writers")]
        public int[] idsWriters { get; set; }
        [Display(Name = "Writers")]
        public IEnumerable<Writer> ListWriters { get; internal set; }

        //Movie Comments List 
        [ForeignKey("Comments")]
        [Display(Name = "Comments")]
        public int[] idsComments { get; set; }
        public IEnumerable<Comment> ListComments { get; internal set; }
        
        //Movie Rates List 
        [ForeignKey("Rates")]
        [Display(Name = "Rates")]
        public int[] idsRates { get; set; }
        public IEnumerable<Rate> ListRates { get; internal set; }

        //Part w/the total 

        //Movie All CHARACTERS List and ids 
        [ForeignKey("All Characters")]
        public int[] idsAllCharacters { get; set; }
        public IEnumerable<Characters> ListAllcharacters { get; set; }

        //Movie All dIRECTORS List and ids 
        [ForeignKey("All Directors")]  
        public int[] idsAllDirectores { get; set; }
        public IEnumerable<Director> ListAllDirectors { get; set; }

        //Movie All Writers List and ids 
        [ForeignKey("All Writers")]
        public int[] idsAllWriters { get; set; }
        public IEnumerable<Writer> ListAllWriters { get; internal set; }

        //Movie All Comments List 
        [ForeignKey("All Comments")]
        public int[] idsAllComments { get; set; }
        public IEnumerable<Comment> ListAllComments { get; internal set; }

        //Movie All Rates List and ids 
        [ForeignKey("All Rates")]
        public int[] idsAllRates { get; set; }
        public IEnumerable<Rate> ListAllRates { get; internal set; }


    }
}