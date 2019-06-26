using CoderGirl_MVCMovies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<Director> Directors { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options) {   }
    }
}
