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
        public int Id { get; set; }

        public void Persist()
        {
            Models.MovieRating movieRating = new Models.MovieRating
            {
                //added the ID below, but not sure needed?
                Id = this.Id,
                MovieName = this.MovieName,
                Rating = this.Rating
            };
            RepositoryFactory.GetMovieRatingRepository().Save(movieRating);
        }
    }

    
}
