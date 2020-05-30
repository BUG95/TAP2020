using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class SQLMovieRepository : IMovieRepository
    {
        private readonly MovieDbContext context;

        public SQLMovieRepository(MovieDbContext context)
        {
            this.context = context;
        }

        public Movie Add(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Movie Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public Movie GetMovie(int Id)
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie movieChanges)
        {
            throw new NotImplementedException();
        }
    }
}
