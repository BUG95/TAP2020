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
    public class MovieDirectionsController : Controller
    {
        private readonly MovieDbContext _context;

        public MovieDirectionsController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: MovieDirections
        public async Task<IActionResult> Index()
        {
            var movieDbContext = _context.MovieDirections.Include(m => m.Director).Include(m => m.Movie);
            return View(await movieDbContext.ToListAsync());
        }

        // GET: MovieDirections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirection = await _context.MovieDirections
                .Include(m => m.Director)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieDirection == null)
            {
                return NotFound();
            }

            return View(movieDirection);
        }

        // GET: MovieDirections/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "DirectorId");
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId");
            return View();
        }

        // POST: MovieDirections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectorId,MovieId")] MovieDirection movieDirection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieDirection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "DirectorId", movieDirection.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieDirection.MovieId);
            return View(movieDirection);
        }

        // GET: MovieDirections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirection = await _context.MovieDirections.FindAsync(id);
            if (movieDirection == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "DirectorId", movieDirection.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieDirection.MovieId);
            return View(movieDirection);
        }

        // POST: MovieDirections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DirectorId,MovieId")] MovieDirection movieDirection)
        {
            if (id != movieDirection.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieDirection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieDirectionExists(movieDirection.MovieId))
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
            ViewData["DirectorId"] = new SelectList(_context.Directors, "DirectorId", "DirectorId", movieDirection.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", movieDirection.MovieId);
            return View(movieDirection);
        }

        // GET: MovieDirections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirection = await _context.MovieDirections
                .Include(m => m.Director)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieDirection == null)
            {
                return NotFound();
            }

            return View(movieDirection);
        }

        // POST: MovieDirections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDirection = await _context.MovieDirections.FindAsync(id);
            _context.MovieDirections.Remove(movieDirection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieDirectionExists(int id)
        {
            return _context.MovieDirections.Any(e => e.MovieId == id);
        }
    }
}
