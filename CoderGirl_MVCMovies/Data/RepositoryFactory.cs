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

        // TODO: Be sure the factory is providing the right class for each
        // TODO: Note that you have similar named classes - be sure not to mix them up
        public static IMovieRatingRepository GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new MovieRatingRepository();
            return movieRatingRepository;
        }

        public static IMovieRepository GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();
            return movieRepository;
        }
    }
}
