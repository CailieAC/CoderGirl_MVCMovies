using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewsModels.MovieRatings
{
    public class MovieRatingCreateViewModel
    {
        private string ratings = "12345";

        [Required]
        public int MovieId { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public int Rating { get; set; }
        public SelectList Ratings { get { return GetRatings(); } }


        private SelectList GetRatings()
        {
            var ratingSelectListItems = ratings.Select(r => new SelectListItem(r.ToString(), r.ToString()));
            return new SelectList(ratingSelectListItems);
        }

        internal void Persist(MoviesDbContext context)
        {
            MovieRating rating = new MovieRating
            {
                MovieId = this.MovieId,
                Rating = this.Rating
            };
            context.Add(rating);
            context.SaveChanges();
        }
    }
}
