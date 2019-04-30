using HtmlAgilityPack;
using System;
using Xunit;
using CoderGirl_MVCMovies.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test
{
    public class TestMovieRatingController
    {
        private MovieRatingController controller;

        public TestMovieRatingController()
        {
            controller = new MovieRatingController();
        }

        [Fact]
        public void Test_GET_Create_ReturnsViewResult_ForCreate()
        {           
            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }

        [Theory]
        [InlineData("Star Wars", "5")]
        [InlineData("The Princess Bride", "4")]
        public void Test_POST_Create_RedirectsTo_GETDetails_WithInputValues(string movieName, string rating)
        {
            IActionResult result = controller.Create(movieName, rating);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal(new KeyValuePair<string, object>("movieName", movieName), redirect.RouteValues.First());
            Assert.Equal(new KeyValuePair<string, object>("rating", rating), redirect.RouteValues.Skip(1).Take(1).First());
        }

        [Theory]
        [InlineData("Star Wars", "5")]
        [InlineData("The Princess Bride", "4")]
        public void Test_GET_Details(string movieName, string rating)
        {
            string expectedString = $"{movieName} has a rating of {rating}";

            IActionResult result = controller.Details(movieName, rating);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
        }
    }
}
