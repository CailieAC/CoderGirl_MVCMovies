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
        //TODO: Go through and convert all the repository references to IRepository like this
        static IModelRepository movieRepository = RepositoryFactory.GetMovieRepository();
        static IModelRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        public IActionResult Index()
        {
            List<Movie> movies = movieRepository.GetModels().Cast<Movie>().ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Directors = directorRepository.GetModels();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            //Server side validation can take place in Post. Or can go in the Movie Model.
            //Should probably go in Movie Model in the future.
            //TODO: Figure out if user not entering a movie name be null or empty?
            if (String.IsNullOrWhiteSpace(movie.Name))
            {
                ModelState.AddModelError("Name", "Name must be included");
            }
            if (movie.Year < 1888 || movie.Year > DateTime.Now.Year) 
            {
                ModelState.AddModelError("Year", "Not a valid year"); 
            }
            if (ModelState.ErrorCount>0)
            {
                //Have to give them the directors again
                ViewBag.Directors = directorRepository.GetModels();
                return View(movie);
            }

            movieRepository.Save(movie);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //List<Movie> movies = movieRepository.GetModels().Cast<Movie>().ToList();
            //Movie movie = movieRepository.GetById(id);
            //TODO: Not sure if below is correct? Should be casting to Movie?
            IModel movie = movieRepository.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(int id, Movie movie)
        {
            //since id is not part of the edit form, it isn't included in the model, thus it needs to be set from the route value
            //there are alternative patterns for doing this - for one, you could include the id in the form but make it hidden
            //feel free to experiment - the tests wont' care as long as you preserve the id correctly in some manner
            movie.Id = id; 
            movieRepository.Update(movie);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            movieRepository.Delete(id);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}