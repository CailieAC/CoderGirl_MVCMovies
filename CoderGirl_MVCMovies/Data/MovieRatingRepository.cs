using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        private static List<MovieRating> movieRatings = new List<MovieRating>();
        private static int nextId = 1;

        public MovieRating GetById(int id)
        {
            return movieRatings.SingleOrDefault(m => m.Id == id);
        }

        public List<MovieRating> GetMovieRatings()
        {
            return movieRatings;
        }

        public int Save(MovieRating movieRating)
        {
            movieRating.Id = nextId;
            movieRatings.Add(movieRating);
            nextId++;
            return movieRating.Id;
        }

        public void Update(MovieRating movieRating)
        {
            Delete(movieRating.Id);
            movieRatings.Add(movieRating);
        }

        public void Delete(int id)
        {
            movieRatings.RemoveAll(m => m.Id == id);
        }
    }
}
