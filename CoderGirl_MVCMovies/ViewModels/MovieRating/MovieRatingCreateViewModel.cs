using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.MovieRating
{
    public class MovieRatingCreateViewModel
    {
        public string MovieName { get; set; }
        public int Rating { get; set; }
        public int MovieId { get; set; }

        public void Persist()
        {
            Models.MovieRating movieRating = new Models.MovieRating
            {
                MovieName = this.MovieName,
                MovieId = this.MovieId,
                Rating = this.Rating
            };
            //TODO: Add the movierating to the movieobject's list of movieratings
            Models.Movie movie = (Models.Movie)RepositoryFactory.GetMovieRepository().GetById(MovieId);
            movie.Ratings.Add(Rating);

            RepositoryFactory.GetMovieRatingRepository().Save(movieRating);
        }
    }

    
}
