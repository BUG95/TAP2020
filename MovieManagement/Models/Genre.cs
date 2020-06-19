using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Display(Name = "Genre")]
        public Genres GenreTitle { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; }
    }
}