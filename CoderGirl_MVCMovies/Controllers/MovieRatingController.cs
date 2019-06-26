using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.ViewModels.MovieRatings;
using CoderGirl_MVCMovies.ViewsModels.MovieRatings;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieRatingController : Controller
    {
        private readonly MoviesDbContext context;

        public MovieRatingController(MoviesDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create(int movieId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int movieId, MovieRatingCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Persist(context);
            return RedirectToAction(controllerName: "Movie", actionName: "Index");
        }
    }
}