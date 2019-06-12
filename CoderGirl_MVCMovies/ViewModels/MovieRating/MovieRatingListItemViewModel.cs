using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.MovieRating
{
    public class MovieRatingListItemViewModel
    {
        public static List<MovieRatingListItemViewModel> GetDirectorList()
        {
            return RepositoryFactory.GetMovieRatingRepository()
                .GetModels()
                .Cast<Models.MovieRating>()
                .Select(rating => GetMovieRatingListItemFromMovieRating(rating))
                .ToList();
        }

        public static List<MovieRatingListItemViewModel> GetMovieRatingList()
        {
            return RepositoryFactory.GetMovieRatingRepository()
               .GetModels()
               .Cast<Models.MovieRating>()
               .Select(movieRating => GetMovieRatingListItemFromMovieRating(movieRating))
               .ToList(); 
        }

        private static MovieRatingListItemViewModel GetMovieRatingListItemFromMovieRating(Models.MovieRating movieRating)
        {
            return new MovieRatingListItemViewModel
            {
                Id = movieRating.Id,
                MovieName = movieRating.MovieName,
                MovieRating = movieRating.Rating
            };
        }

        public int Id { get; set; }
        public string MovieName { get; set; }

        [Display(Name = "Rating")]
        public int MovieRating { get; set; }
    }
}
