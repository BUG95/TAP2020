using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Models;

namespace MovieManagement.Controllers
{
    public class MovieCastingsController : Controller
    {
        private readonly MovieDbContext _context;

        public MovieCastingsController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: MovieCastings
        public async Task<IActionResult> Index()
        {
            var movieDbContext = _context.MovieCastings.Include(m => m.Actor).Include(m => m.Movie);
            return View(await movieDbContext.ToListAsync());
        }

        // GET: MovieCastings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCasting = await _context.MovieCastings
                .Include(m => m.Actor)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieCasting == null)
            {
                return NotFound();
            }

            return View(movieCasting);
        }

        // GET: MovieCastings/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorId");
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId");
            return View();
        }

        // POST: MovieCastings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,ActorId,Role")] MovieCasting movieCasting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieCasting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorId", movieCasting.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieCasting.MovieId);
            return View(movieCasting);
        }

        // GET: MovieCastings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCasting = await _context.MovieCastings.FindAsync(id);
            if (movieCasting == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorId", movieCasting.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieCasting.MovieId);
            return View(movieCasting);
        }

        // POST: MovieCastings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,ActorId,Role")] MovieCasting movieCasting)
        {
            if (id != movieCasting.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieCasting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieCastingExists(movieCasting.MovieId))
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
            ViewData["ActorId"] = new SelectList(_context.Actors, "ActorId", "ActorId", movieCasting.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieCasting.MovieId);
            return View(movieCasting);
        }

        // GET: MovieCastings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieCasting = await _context.MovieCastings
                .Include(m => m.Actor)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieCasting == null)
            {
                return NotFound();
            }

            return View(movieCasting);
        }

        // POST: MovieCastings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieCasting = await _context.MovieCastings.FindAsync(id);
            _context.MovieCastings.Remove(movieCasting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieCastingExists(int id)
        {
            return _context.MovieCastings.Any(e => e.MovieId == id);
        }
    }
}
