using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using CoderGirl_MVCMovies.ViewModels.Movie;
using CoderGirl_MVCMovies.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        static IRepository movieRepository = RepositoryFactory.GetMovieRepository();
        static IRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        public IActionResult Index()
        {
            //keep this as a list passing in, but pass in a list of like MovieListViewModel
            //or MovieIndexViewModel. Should only include the info we are displaying in the table
            //table has director name, but not id. 
            List<Movie> movies = movieRepository.GetModels().Cast<Movie>().ToList();
            
            //New: List<MovieListItemViewModel> movies = movieRepository.GetModels().Cast<Movie>().ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MovieCreateViewModel viewModel = MovieCreateViewModel.GetMovieCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(MovieCreateViewModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Name must be included");
            }
            if(model.Year < 1888 || model.Year > DateTime.Now.Year)
            {
                ModelState.AddModelError("Year", "Year is not valid");
            }

            if(ModelState.ErrorCount > 0)
            {
                model.Directors = directorRepository.GetModels().Cast<Director>().ToList();
                return View(model);
            }
 
            //movieRepository.Save(model);
            model.Persist();
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Movie movie = (Movie)movieRepository.GetById(id);
            //return View(movie);

            //TODO constructor or factory class will need to take an Id to find the proper movie
            MovieEditViewModel model = MovieEditViewModel.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieEditViewModel model)
        {
            //since id is not part of the edit form, it isn't included in the model, thus it needs to be set from the route value
            //there are alternative patterns for doing this - for one, you could include the id in the form but make it hidden
            //feel free to experiment - the tests wont' care as long as you preserve the id correctly in some manner
            //movie.Id = id; 
            //movieRepository.Update(movie);
            model.Persist(id);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //Don't need a view model for this because there isn't an actual page view for Delete
            movieRepository.Delete(id);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}