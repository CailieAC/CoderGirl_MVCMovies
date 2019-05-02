using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
    }
}
