using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieListItemViewModel
    {
        public static List<MovieListItemViewModel> GetMovies(MoviesDbContext context)
        {
            return context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.DirectorMovies)
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
            this.DirectorName = getDirectorNames(movie);
            this.AverageRating = GetAverageRating(movie);
            this.NumberOfRatings = movie.Ratings.Count;
        }

        private string getDirectorNames(Movie movie)
        {
            return string.Join(", ", movie.DirectorMovies
                .Select(dm => dm.Director.FullName));
        }

        private static string GetAverageRating(Movie movie)
        {
            return movie.Ratings.Count > 0 
                ? Math.Round(movie.Ratings.Average(x => x.Rating), 2).ToString() 
                : "none";
        }
    }
}
