using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieListItemViewModel
    {
        public static List<MovieListItemViewModel> GetMovies(MoviesDbContext factory)
        {
            return factory.GetMovieRepository()
                .GetModels()
                .Select(m => new MovieListItemViewModel(m))
                .ToList();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Year { get; set; }
        public string DirectorName { get; set; }
        public string AverageRating { get; set; }
        public int NumberOfRatings { get; set; }

        public MovieListItemViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Year = movie.Year;
            this.DirectorName = movie.Director.FullName;
            this.AverageRating = movie.Ratings.Count > 0 ? Math.Round(movie.Ratings.Average(x => x.Rating), 2).ToString() : "none";
            this.NumberOfRatings = movie.Ratings.Count;
        }
    }
}
