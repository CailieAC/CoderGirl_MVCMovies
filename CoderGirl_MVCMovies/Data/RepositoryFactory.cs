using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public static class RepositoryFactory
    {
        //private static IMovieRatingRepository movieRatingRepository;
        //private static IMovieRepository movieRepository;
        //private static IDirectorRepository directorRepository;
        private static IRepository movieRatingRepository;
        private static IRepository movieRepository;
        private static IRepository directorRepository;

        public static IRepository GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new MovieRatingRepository();
            return movieRatingRepository;
        }

        public static IRepository GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();
            return movieRepository;
        }

        public static IRepository GetDirectorRepository()
        {
            if (directorRepository == null)
                directorRepository = new DirectorRepository();
            return directorRepository;
        }
    }
}
