using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieListItemViewModel
    {
        public int Id { get; set; }

        [Display(Name="Movie Name")]
        public string Name { get; set; }
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }
        public int Year { get; set; }
        public List<int> Ratings { get; set; }

        public string AverageRating { get; set; }
        public int NumberOfRatings { get; set; } 
        
        public static List<MovieListItemViewModel> GetMovieList()
        {
            return RepositoryFactory.GetMovieRepository()
                .GetModels()
                .Cast<Models.Movie>()
                .Select(movie => GetMovieListItemFromMovie(movie))
                .ToList();
        }

        private static MovieListItemViewModel GetMovieListItemFromMovie(Models.Movie movie)
        {
            string average = "none";
            int count = 0;
            if (movie.Ratings != null)
            {
                count = movie.Ratings.Count;
                if (count > 0)
                {
                    average = movie.Ratings.Average().ToString();
                }
            }

            return new MovieListItemViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                DirectorName = movie.DirectorName,
                Year = movie.Year,
                Ratings = movie.Ratings,
                AverageRating = average,
                NumberOfRatings = count
            };
        }
    }
}