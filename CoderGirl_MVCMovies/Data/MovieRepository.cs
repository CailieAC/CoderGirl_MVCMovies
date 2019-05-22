using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRepository : BaseRepository
    {
        static IMovieRatingRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        static IDirectorRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        public override IModel GetById(int id)
        {
            //Movie movie = models.SingleOrDefault(m => m.Id == id);
            //must cast IModel returned by GetById to Movie class
            Movie movie = (Movie)base.GetById(id);
            movie = SetMovieRatings(movie);
            movie = SetDirectorName(movie);
            return movie;
        }

        public override List<IModel> GetModels()
        {
            return models.Select(movie => SetMovieRatings(movie))
                .Select(movie => SetDirectorName(movie)).ToList();
        }

        private Movie SetMovieRatings(Movie movie)
        {
            List<int> ratings = ratingRepository.GetMovieRatings()
                                                .Where(rating => rating.MovieId == movie.Id)
                                                .Select(rating => rating.Rating)
                                                .ToList();
            movie.Ratings = ratings;
            return movie;
        }

        private Movie SetDirectorName(Movie movie)
        {
            Director director = directorRepository.GetById(movie.DirectorId);
            movie.DirectorName = director.FullName;
            return movie;
        }
    }
}
