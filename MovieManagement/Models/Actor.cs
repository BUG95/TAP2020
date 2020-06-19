using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        [Display(Name = "Actor First Name")]
        public string ActorFirstName { get; set; }
        [Display(Name = "Actor Last Name")]
        public string ActorLastName { get; set; }
        [Display(Name = "Actor Gender")]
        public string ActorGender { get; set; }
        
        public IList<MovieCasting> MovieCastings { get; set; }
    }
}
