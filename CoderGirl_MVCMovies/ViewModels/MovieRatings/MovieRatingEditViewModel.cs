using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.MovieRatings
{
    public class MovieRatingEditViewModel
    {
        private string ratings = "12345";

        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Rating { get; set; }
        public SelectList Ratings { get { return GetRatings(); } }

        public MovieRatingEditViewModel(int id, MoviesDbContext context)
        {
            MovieRating rating = context.MovieRatings
                .Include(m => m.Movie)
                .SingleOrDefault(m => m.Id == id);
            this.Id = rating.Id;
            this.MovieId = rating.MovieId;
            this.MovieName = rating.Movie.Name;
            this.Rating = rating.Rating;
        }

        private SelectList GetRatings()
        {
            var ratingSelectListItems = ratings.Select(r => new SelectListItem(r.ToString(), r.ToString(), this.Rating == r));
            return new SelectList(ratingSelectListItems);
        }
    }
}
