using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class DirectorController : Controller
    {
        private IDirectorRepository directorRepository = RepositoryFactory.GetDirectorRepository();

        [HttpGet]
        public IActionResult Index()
        {
            List<Director> directors = directorRepository.GetDirectors();
            return View(directors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Director director)
        {
            if (String.IsNullOrWhiteSpace(director.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name must be included");
            }
            if (String.IsNullOrWhiteSpace(director.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name must be included");
            }
            //if (String.IsNullOrWhiteSpace(director.Nationality))
            //{
            //    director.Nationality = "unknown";
            //}
            if (ModelState.ErrorCount > 0)
            {
                //Have to give them the directors again
                ViewBag.Directors = directorRepository.GetDirectors();
                return View(director);
            }

            directorRepository.Save(director);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}