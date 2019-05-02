using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public static class RepositoryFactory
    {
        private static IMovieRatingRepository movieRatingRepository;
        private static IMovieRepository movieRepository;

        public static IMovieRatingRepository GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new MovieRatingRepository();// TODO: new up your implementation class here
            return movieRatingRepository;
        }

        public static IMovieRepository GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();// TODO: new up your implementation class here
            return movieRepository;
        }
    }
}
