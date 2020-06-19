using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.ViewModels
{
    public class AddMovieViewModel
    {
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
        public Director Director { get; set; }
        public SelectList Genres { get; set; }
        public Genre Genre { get; set; }
        public IFormFile Photo { get; set; }
    }
}
