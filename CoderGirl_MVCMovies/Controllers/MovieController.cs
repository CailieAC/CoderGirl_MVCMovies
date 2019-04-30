using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        public static Dictionary<int, string> movies = new Dictionary<int, string>();
        private static int nextIdToUse = 1;

        public IActionResult Index()
        {
            ViewBag.Movies = movies;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string movie)
        {
            movies.Add(nextIdToUse, movie);
            nextIdToUse++;
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}