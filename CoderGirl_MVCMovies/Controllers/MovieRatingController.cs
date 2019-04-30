﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieRatingController : Controller
    {
        private IMovieRatingRepository repository = RepositoryFactory.GetMovieRatingRepository();
        public static List<Movie> movies = new List<Movie>();

        private string htmlForm = @"
            <form method='post'>
                <input name='movieName' />
                <select name='rating'>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>                    
                </select>
                <button type='submit'>Rate it</button>
            </form>";

        private void PopulateMovieList()
        {
            foreach (int id in repository.GetIds())
            {
                Movie mov = new Movie();
                mov.Id = movies.Count + 1;
                mov.Name = repository.GetMovieNameById(id);
                mov.Rating = repository.GetRatingById(id);
                movies.Add(mov);
            }

            //movies.Add(
            //    new Movie { Name = "The Matrix", Rating = 5, Id = 1 }
            //);
            //movies.Add(
            //    new Movie { Name = "The Matrix Reloaded", Rating = 3, Id = 2 }
            //);
            //movies.Add(
            //    new Movie { Name = "The Matrix The really bad one", Rating = 1, Id = 3 }
            //);
        }

        /// TODO: Create a view Index. This view should list a table of all saved movie names with associated average rating
        /// TODO: Be sure to include headers for Movie and Rating
        /// TODO: Each tr with a movie rating should have an id attribute equal to the id of the movie rating
        public IActionResult Index()
        {
            PopulateMovieList();
            ViewBag.Movies = movies;
            return View();
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = movies;
            return View();
        }

        // TODO: Redirect passing only the id of the created movie/rating
        [HttpPost]
        public IActionResult Create(string movieName, string rating)
        {
            int id = repository.SaveRating(movieName, int.Parse(rating));
            return RedirectToAction(actionName: nameof(Details), routeValues: new { id });
        }

        // TODO: The Details method should take an int parameter which is the id of the movie/rating to display.
        // TODO: Create a Details view which displays the formatted string with movie name and rating in an h2 tag. 
        // TODO: The Details view should include a link to the MovieRating/Index page
        [HttpGet]
        public IActionResult Details(string movieName, string rating)
        {
            return Content($"{movieName} has a rating of {rating}");
        }
    }
}