using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        private MoviesDbContext context;

        public MovieController(MoviesDbContext context)
        {
            this.context = context;
        }

        
        public IActionResult Index()
        {
            List<MovieListItemViewModel> movies = MovieListItemViewModel.GetMovies(context);
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MovieCreateViewModel model = new MovieCreateViewModel(context);
            return View(model);
        }
                
        [HttpPost]
        public IActionResult Create(MovieCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ResetDirectorList(context);
                return View(model);
            }

            model.Persist(context);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id, MoviesDbContext context)
        {            
            return View(new MovieEditViewModel(id, context));
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieEditViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                movie.ResetDirectorList(context);
                return View(movie);
            }

            movie.Persist(id, context);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            context.Remove(context.Movies.Find(id));
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}