using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public static class RepositoryFactory
    {
        private static BaseRepository<MovieRating> movieRatingRepository;
        private static BaseRepository<Movie> movieRepository;
        private static BaseRepository<Director> directorRepository;

        public static BaseRepository<MovieRating> GetMovieRatingRepository()
        {
            if (movieRatingRepository == null)
                movieRatingRepository = new BaseRepository<MovieRating>();
            return movieRatingRepository;
        }

        public static IRepository<Movie> GetMovieRepository()
        {
            if (movieRepository == null)
                movieRepository = new MovieRepository();
            return movieRepository;
        }

        public static IRepository<Director> GetDirectorRepository()
        {
            if (directorRepository == null)
                directorRepository = new DirectorRepository();
            return directorRepository;
        }
    }
}
