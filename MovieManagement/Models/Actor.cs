using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public string ActorGender { get; set; }
        
        public IList<MovieCasting> MovieCastings { get; set; }
    }
}
