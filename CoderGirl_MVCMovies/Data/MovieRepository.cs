using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    internal class MovieRepository : ModelRepository
    {
        static IModelRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        static IModelRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        public override IModel GetById(int id)
        {
            //Movie movie = models.SingleOrDefault(m => m.Id == id);
            //must cast IModel returned by GetById to Movie class to complete the rest of the code
            //base is asking the parent for it's code when you're in a child class
            Movie movie = (Movie)base.GetById(id);
            movie = SetMovieRatings(movie);
            movie = SetDirectorName(movie);
            //can return movie type, since it's also an IModel type
            return movie;
        }

        public override List<IModel> GetModels()
        {
            //TODO need to cast, but it's a list, so it's different than above - there is a linq method called cast
            //see link to explanation from David
            /*
            return models.Select(movie => SetMovieRatings(movie))
                .Select(movie => SetDirectorName(movie)).ToList();
            */
            
            //List<Director> directors = directorRepository.GetModels().Cast<Director>().ToList();
            List<Movie> movies = models.Select(movie => SetMovieRatings((Movie)movie))
                .Select(movie => SetDirectorName(movie)).ToList();
            return movies.Cast<IModel>().ToList();
            
        }

        private Movie SetMovieRatings(Movie movie)
        {
            List<int> ratings = ratingRepository.GetModels()
                                                .Where(rating => rating.Id == movie.Id).Cast<MovieRating>()
                                                .Select(rating => rating.Rating)
                                                .ToList();
            movie.Ratings = ratings;
            return movie;
        }

        private Movie SetDirectorName(Movie movie)
        {
            Director director = directorRepository.GetById(movie.Id);
            movie.DirectorName = director.FullName;
            return movie;
        }
    }
}
