using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    internal class MovieRepository<T> : BaseRepository<T>
    {
        static IRepository<MovieRating> ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        static IRepository<Director> directorRepository = RepositoryFactory.GetDirectorRepository();

        public override IModel GetById(int id)
        {
            Movie movie = (Movie)base.GetById(id);
            movie = SetMovieRatings(movie);
            movie = SetDirectorName(movie);
            return movie;
        }

        public override List<IModel> GetModels()
        {
            return base.GetModels()
                .Select(movie => SetDirectorName((Movie)movie))
                .Select(movie => movie as IModel).ToList();
        }
       
        private Movie SetMovieRatings(Movie movie)
        {
            List<int> ratings = ratingRepository.GetModels().Cast<MovieRating>()
                                                .Where(rating => rating.MovieId == movie.Id)
                                                .Select(rating => rating.Rating)
                                                .ToList();
            movie.Ratings = ratings;
            return movie;
        }

        private Movie SetDirectorName(Movie movie)
        {
            Director director = (Director)directorRepository.GetById(movie.DirectorId);
            movie.DirectorName = director.FullName;
            return movie;
        }
    }
}
