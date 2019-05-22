using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieRatingController : Controller
    {
        private IMovieRatingRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        private IMovieRepository movieRespository = RepositoryFactory.GetMovieRepository();

       public IActionResult Index()
        {
            List<MovieRating> movieRatings = ratingRepository.GetMovieRatings();
            return View(movieRatings);
        }

        [HttpGet]
        public IActionResult Create(int movieId)
        {
            string movieName = movieRespository.GetById(movieId).Name;
            MovieRating movieRating = new MovieRating();
            movieRating.MovieId = movieId;
            movieRating.MovieName = movieName;
            return View(movieRating);
        }

        [HttpPost]
        public IActionResult Create(int movieId, MovieRating movieRating)
        {
            ratingRepository.Save(movieRating);
            return RedirectToAction(controllerName: nameof(Movie), actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            MovieRating movieRating = ratingRepository.GetById(id);
            return View(movieRating);
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieRating movieRating)
        {
            movieRating.Id = id;
            ratingRepository.Update(movieRating);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ratingRepository.Delete(id);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}