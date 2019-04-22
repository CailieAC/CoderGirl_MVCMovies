using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoderGirl_MVCMovies.Controllers
{
    public class MovieRatingController : Controller
    {
        private string htmlForm = @"
            <form method = 'POST'>
                <input name ='movieName'>
                <select name = 'rating'>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                </select>
                <button type='submit'>Submit</button>
            </form>";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return Content(htmlForm, "text/html");
        }

        [HttpPost]
        public IActionResult Create(string movieName, string rating)
        {
            return RedirectToAction(actionName: nameof(Details), routeValues: new { movieName, rating });
        }

        [HttpGet]
        public IActionResult Details(string movieName, string rating)
        {
            return Content($"{movieName} has a rating of {rating}");
        }

        //Create a string html template for a form
        // with method of post
        // an input with name "movieName" 
        // a select with name "rating" and that contains options for 1 through 5
        // a button with type of submit

        // Create an GET Action for Create which returns the above template as Content

        // Create a POST Action for Create which takes...
        // two string parameters which match the names of the input and select names in the template
        // The method should redirect to a the GET Action for Details. Be sure to pass the parameters as route values.

        //Create a GET Action for Details
        // Details should take the parameters passed by the POST Action for Create
        // Details should return a string as Content. This string should be in the format "{moveName} has a rating of {rating}"
    }
}