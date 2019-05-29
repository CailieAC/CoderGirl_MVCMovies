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
        //private IMovieRatingRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        //private IMovieRepository movieRespository = RepositoryFactory.GetMovieRepository();
        static IModelRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();
        static IModelRepository movieRespository = RepositoryFactory.GetMovieRepository();

        public IActionResult Index()
        {
            List<MovieRating> movieRatings = ratingRepository.GetModels().Cast<MovieRating>().ToList();
            return View(movieRatings);
        }

        [HttpGet]
        public IActionResult Create(int movieId)
        {
            /* EXAMPLES of Casting for reference
            List<Movie> movies = models.Select(movie => SetMovieRatings((Movie)movie))
                .Select(movie => SetDirectorName(movie)).ToList();
            return movies.Cast<IModel>().ToList();
            */
            Movie movie = (Movie)movieRespository.GetById(movieId);
            string movieName = movie.Name;
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
            MovieRating movieRating = (MovieRating)ratingRepository.GetById(id);
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