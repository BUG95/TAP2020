using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MovieManagement.Models;
using MovieManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MovieManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public MoviesController(MovieDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Movies
        [AllowAnonymous]
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            var genreQuery = await _context.MovieGenres
                .OrderBy(x => x.Genre.GenreTitle)
                .Select(x => x.Genre.GenreTitle)
                .Distinct()
                .ToListAsync();

            var movies = from movie in _context.Movies
                         select movie;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.MovieTitle.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(mg => mg.MovieGenres.Any(g => g.Genre.GenreTitle.ToString() == movieGenre));
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(genreQuery.Distinct()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        [AllowAnonymous]
        // GET: Movies/View/5
        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(mc => mc.MovieCastings)
                    .ThenInclude(actor => actor.Actor)
                .Include(md => md.MovieDirections)
                    .ThenInclude(director => director.Director)
                .Include(mg => mg.MovieGenres)
                    .ThenInclude(genre => genre.Genre)
                .SingleOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,MovieTitle,MovieYear,MovieLanguage,MoviePath,MovieCoverImage")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,MovieTitle,MovieYear,MovieLanguage,MoviePath,MovieCoverImage")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            var addMovieViewModel = new AddMovieViewModel
            {
                Genres = new SelectList(Enum.GetValues(typeof(Genres)))
            };
            return View(addMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(AddMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName; //guid = global unique identifier
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    model.Movie.MovieCoverImage = uniqueFileName;
                }

                int movieId = _context.Movies.Last().MovieId + 1;
                int actorId = _context.Actors.Last().ActorId + 1;
                int directorId = _context.Directors.Last().DirectorId + 1;


                
                _context.Add(model.Movie);
                _context.Add(model.Actor);
                _context.Add(model.Director);

                MovieCasting movieCasting = new MovieCasting
                {
                    MovieId = movieId,
                    ActorId = actorId,
                    Actor = model.Actor,
                    Movie = model.Movie,
                    Role = ""
                };
                MovieDirection movieDirection = new MovieDirection
                {
                    MovieId = movieId,
                    DirectorId = directorId,
                    Director = model.Director,
                    Movie = model.Movie
                };
                
                Array _genres = Enum.GetValues(typeof(Genres));
                int genreId = 0;

                foreach(int gen in _genres)
                {
                    if(Enum.GetName(typeof(Genres), gen) == model.Genre.GenreTitle.ToString())
                    {
                        break ;
                    }
                    genreId++;
                }
                Genre g = new Genre {GenreTitle=(Genres) Enum.Parse(typeof(Genres), model.Genre.GenreTitle.ToString())};
                MovieGenre movieGenre = new MovieGenre
                {
                    MovieId = movieId,
                    GenreId = _context.Genres.Last().GenreId + 1,
                    Movie = model.Movie,
                    Genre = g
                };
               
                _context.Add(movieCasting);
                _context.Add(movieDirection);
                _context.Add(movieGenre);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}
