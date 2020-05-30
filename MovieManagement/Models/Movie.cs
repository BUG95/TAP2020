using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Movie
    {
        public Movie(string movieTitle, int movieYear, string movieLanguage, string moviePath)
        {
            MovieTitle = movieTitle;
            MovieYear = movieYear;
            MovieLanguage = movieLanguage;
            MoviePath = moviePath;
        }

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int MovieYear { get; set; }
        public string MovieLanguage { get; set; }
        public string MoviePath { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; }
        public IList <MovieCasting> MovieCastings { get; set; }
        public IList<MovieDirection> MovieDirections { get; set; }
    }
}
