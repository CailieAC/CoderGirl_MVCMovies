using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRepository : IMovieRepository
    {
        static List<Movie> movies = new List<Movie>();
        static int nextId = 1;

        public Movie GetById(int id)
        {
            return movies.SingleOrDefault(m => m.Id == id);
        }

        public List<Movie> GetMovies()
        {
            return movies;
        }

        public int Save(Movie movie)
        {
            movie.Id = nextId;
            nextId++;
            movies.Add(movie);
            return movie.Id;
        }
    }
}
