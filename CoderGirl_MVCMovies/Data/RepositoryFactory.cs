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
        private static IModelRepository movieRatingRepository;
        private static IModelRepository movieRepository;
        private static IModelRepository directorRepository;

        public static IModelRepository GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new MovieRatingRepository();
            return movieRatingRepository;
        }

        public static IModelRepository GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();
            return movieRepository;
        }

        public static IModelRepository GetDirectorRepository()
        {
            if (directorRepository == null)
                directorRepository = new DirectorRepository();
            return directorRepository;
        }
    }
}
