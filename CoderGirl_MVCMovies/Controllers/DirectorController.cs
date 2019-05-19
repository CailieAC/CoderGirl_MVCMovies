using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public static IDirectorRepository directorRepository = RepositoryFactory.GetDirectorRepository();

    public class DirectorController : Controller
    {
        public IActionResult Index()
        {
            List<Director> directors = DirectorRepository.GetDirectors();
            return View(directors);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}