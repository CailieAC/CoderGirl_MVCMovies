using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        //public static Dictionary<int, string> movies = new Dictionary<int, string>();
        private IMovieRepository movieRepository = RepositoryFactory.GetMovieRepository();
        private static int nextIdToUse = 1; 

        public IActionResult Index()
        {
            //Have to pass something in to the model, or it will give an error
            List<Movie> movies = movieRepository.GetMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            movieRepository.Save(movie);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Movie movie = movieRepository.GetById(id);
            //We can re-use views like this, although not always the best approach
            return View("Create", movie);
        }

        [HttpPost]
        public IActionResult Edit(int id, Movie movie)
        {
            //We can't save it this way (repository.Save)- our save method currenlty assigns new id - need to either edit
            //the method to check for existing Id, or create new edit method
            //TODO: Update movie
            return RedirectToAction(actionName: nameof(Index));
            //post method never returns a view, just redirects
        }
    }
}