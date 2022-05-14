
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;



namespace MeToMe.Models
{
    public class Movie
    {
        [Key]
        public int MovieId {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        public string Genre {get;set;}
        [Required]
        public int ReleseYear {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Cast> CastList {get;set;}
    }

}