using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Models
{
    public class DirectorMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }
        public virtual Movie Movie { get; set; }

    }
}
