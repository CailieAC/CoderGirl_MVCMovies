using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public static class RepositoryFactory
    {
        private static IMovieRatingRepository movieRatingRepository;
        private static IMovieRespository movieRepository;
        private static IDirectorRepository directorRepository;

        public static IMovieRatingRepository GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new MovieRatingRepository();
            return movieRatingRepository;
        }

        public static IMovieRespository GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();
            return movieRepository;
        }

        public static IDirectorRepository GetDirectorRepository()
        {
            if (directorRepository == null)
                directorRepository = new DirectorRepository();
            return directorRepository;
        }
    }
}
