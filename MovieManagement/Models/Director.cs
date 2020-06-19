using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Director
    {
        public int DirectorId { get; set; }
        [Display(Name = "Director First Name")]
        public string DirectorFirstName { get; set; }
        [Display(Name = "Director Last Name")]
        public string DirectorLastName { get; set; }

        public IList<MovieDirection> MovieDirections { get; set; }
    }
}
