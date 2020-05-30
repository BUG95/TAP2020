using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Director
    {
        public int DirectorId { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }

        public IList<MovieDirection> MovieDirections { get; set; }
    }
}
