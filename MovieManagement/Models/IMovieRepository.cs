using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    interface IMovieRepository
    {
        Movie GetMovie(int Id);
        IEnumerable<Movie> GetAllMovies();
        Movie Add(Movie movie);
        Movie Update(Movie movieChanges);
        Movie Delete(int Id);
    }
}
