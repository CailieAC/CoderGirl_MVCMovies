using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using CoderGirl_MVCMovies.ViewModels.Directors;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class DirectorController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var directors = DirectorListItemViewModel.GetDirectorList();
            return View(directors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DirectorCreateViewModel model)
        {
            if (String.IsNullOrWhiteSpace(model.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name must be included");
            }
            if (String.IsNullOrWhiteSpace(model.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name must be included");
            }
            //if (String.IsNullOrWhiteSpace(director.Nationality))
            //{
            //    director.Nationality = "unknown";
            //}
            if (ModelState.ErrorCount > 0)
            {
                return View(model);
            }

            model.Persist();
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DirectorEditViewModel model = DirectorEditViewModel.GetModel(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, DirectorEditViewModel model)
        {
            model.Persist(id);
            return RedirectToAction(actionName: nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            RepositoryFactory.GetDirectorRepository().Delete(id);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}