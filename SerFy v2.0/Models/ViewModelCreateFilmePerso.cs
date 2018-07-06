using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerFy_v2._0.Models
{
    public class ViewModelCreateFilmePerso
    {

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
    }
}


