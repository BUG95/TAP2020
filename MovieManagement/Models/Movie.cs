using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Movie
    {
        
        public int MovieId { get; set; }
        [Display(Name = "Title")]
        public string MovieTitle { get; set; }
        [Display(Name = "Release Year")]
        public int MovieYear { get; set; }
        [Display(Name = "Language")]
        public string MovieLanguage { get; set; }
        public string MoviePath { get; set; }
        public string MovieCoverImage { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; }
        public IList <MovieCasting> MovieCastings { get; set; }
        public IList<MovieDirection> MovieDirections { get; set; }
    }
}
