using CoderGirl_MVCMovies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.MovieRating
{
    public class MovieRatingEditViewModel
    {
        public static MovieRatingEditViewModel GetModel(int id)
        {
            Models.MovieRating movieRating = (Models.MovieRating)RepositoryFactory.GetMovieRatingRepository().GetById(id);
            return new MovieRatingEditViewModel
            {
                MovieName = movieRating.MovieName,
                Rating = movieRating.Rating,
            };
        }

        public string MovieName { get; set; }
        public int Rating { get; set; }

        public void Persist(int id)
        {
            Models.MovieRating movieRating = new Models.MovieRating
            {
                Id = id,
                MovieName = this.MovieName,
                Rating = this.Rating
            };
            RepositoryFactory.GetMovieRatingRepository().Save(movieRating);
        }
    }
}
