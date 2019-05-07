using CoderGirl_MVCMovies;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.IO;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace Test
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class TestMovieRatingViews : IDisposable
    {
        private ChromeDriver driver;
        private const string BASE_URL = "http://localhost:59471";


        public TestMovieRatingViews()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Theory, TestPriority(1)]
        [InlineData("Star Wars", "Lucas", "1976")]
        [InlineData("Princess Bride", "Reiner", "1987")]
        public void TestCreateMovie(string name, string director, string year)
        {
            //add movies to data
            driver.Url = BASE_URL + "/movie/create";
            driver.FindElementByName("Name").SendKeys(name);
            driver.FindElementByName("Director").SendKeys(director);
            driver.FindElementByName("Year").SendKeys(year);
            var movieForm = driver.FindElementByTagName("form");
            var movieSubmit = movieForm.FindElement(By.TagName("button"));
            movieSubmit.Click();

            //verify it redirects to Index
            Assert.Equal(Uri.EscapeUriString(BASE_URL + $"/movie"), driver.Url, true);

            //get table rows
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //Verify the first row has proper headers
            Assert.Equal("Movie Name", headers[0].Text);
            Assert.Equal("Director", headers[1].Text);
            Assert.Equal("Year", headers[2].Text);

            //Verify a row contains expected movie
            Assert.Contains(rows, row => MovieRowMatches(row, name, director, year));
        }

        [Theory, TestPriority(2)]
        [InlineData("Star Wars", "5")]
        [InlineData("Princess Bride", "4")]
        public void TestCreateMovieRating(string name, string rating)
        {
            //navigate to add movie rating page and get elements
            driver.Url = BASE_URL + "/movierating/create";
            var form = driver.FindElementByTagName("form");
            var submit = form.FindElement(By.TagName("button"));
            Assert.Equal("submit", submit.GetAttribute("type"));
            Assert.Equal("Rate Movie", submit.Text);
            var movieSelectInput = new SelectElement(driver.FindElementByName("Movie"));
            var ratingSelectInput = new SelectElement(driver.FindElementByName("Rating"));

            //make selections for input and submit
            movieSelectInput.SelectByText(name);
            ratingSelectInput.SelectByText(rating);
            submit.Click();

            //verify it redirects to Index
            Assert.Equal(Uri.EscapeUriString(BASE_URL + $"/movierating"), driver.Url, true);
        }


        [Theory, TestPriority(3)]
        [InlineData("Star Wars", "5")]
        [InlineData("Princess Bride", "4")]
        public void TestMovieRatingIndexPage(string name, string rating)
        {
            //navigate to movie rating list page
            driver.Url = BASE_URL + "/movierating";

            //verify link to /MovieRating/Create exists and is correct
            var linkToCreate = driver.FindElementById("add-movierating");
            Assert.NotNull(linkToCreate);
            Assert.Equal("a", linkToCreate.TagName);
            Assert.Equal("Rate a Movie", linkToCreate.Text);

            //get table rows
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //Verify the first row has proper headers
            Assert.Equal("Movie", headers[0].Text);
            Assert.Equal("Rating", headers[1].Text);

            //Verify a row contains expected movie/rating combo
            Assert.Contains(rows, row => MovieRatingRowMatches(row, name, rating));
        }

        [Theory, TestPriority(5)]
        [InlineData("Star Wars", "3")]
        [InlineData("Princess Bride", "5")]
        public void TestDeleteMovieRating(string name, string rating)
        {
            //navigate to movie rating list page
            driver.Url = BASE_URL + "/movierating";

            //get table rows
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //get row for test items and click Delete link
            var testRow = rows.Single(r => MovieRatingRowMatches(r, name, rating));
            var deleteLink = GetDeleteLink(testRow);
            var itemId = GetRouteValueForLink(deleteLink);
            deleteLink.Click();

            //Verify no redirect
            Assert.Contains(Uri.EscapeUriString(BASE_URL + $"/movierating"), driver.Url.ToLower());

            //Verify item is deleted
            rows = driver.FindElementsByTagName("tr");
            testRow = rows.Skip(1).SingleOrDefault(r => GetRouteValueForLink(GetDeleteLink(r)) == itemId);
            Assert.Null(testRow);
        }

        [Theory, TestPriority(4)]
        [InlineData("Star Wars", "5", "3")]
        [InlineData("Princess Bride", "4", "5")]
        public void TestEditMovieRating(string name, string rating, string newRating)
        {
            //navigate to movie rating list page
            driver.Url = BASE_URL + "/movierating";

            //get table rows
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //get row for test items and click Edit link
            var testRow = rows.Single(r => MovieRatingRowMatches(r, name, rating));
            var editLink = GetEditLink(testRow);
            var itemId = GetRouteValueForLink(editLink);
            editLink.Click();

            //click Edit and verify we are at correct page
            Assert.Contains(Uri.EscapeUriString(BASE_URL + $"/movierating/edit/"), driver.Url.ToLower());

            //Change values for name and rating then submit
            driver.FindElementById("Movie").SendKeys("badName");
            new SelectElement(driver.FindElementById("Rating")).SelectByText(newRating);
            var submitButton = driver.FindElementByTagName("form").FindElement(By.TagName("button"));
            Assert.Equal("Update Rating", submitButton.Text);
            submitButton.Click();

            //verify it redirects to Index page
            Assert.Contains(Uri.EscapeUriString(BASE_URL + $"/movierating"), driver.Url.ToLower());

            //Find row for item with same id and verify rating but not name is updated
            rows = driver.FindElementsByTagName("tr");
            testRow = rows.Skip(1).SingleOrDefault(r => GetRouteValueForLink(GetEditLink(r)) == itemId);
            Assert.Contains(rows, row => MovieRatingRowMatches(row, name, newRating));
        }

        private static IWebElement GetEditLink(IWebElement testRow)
        {
            return testRow.FindElement(By.LinkText("Edit"));
        }

        private static IWebElement GetDeleteLink(IWebElement testRow)
        {
            return testRow.FindElement(By.LinkText("Delete"));
        }

        private string GetRouteValueForLink(IWebElement editLink)
        {
            var action = editLink.GetAttribute("action").ToString();
            return action.Substring(action.LastIndexOf("/") + 1);
        }

        private bool MovieRatingRowMatches(IWebElement row, string name, string rating)
        {
            var tdElements = row.FindElements(By.TagName("td"));
            if (tdElements.Count < 2) return false;

            return tdElements[0].Text == name && tdElements[1].Text == rating;
        }

        private bool MovieRowMatches(IWebElement row, string name, string director, string year)
        {
            var tdElements = row.FindElements(By.TagName("td"));
            if (tdElements.Count < 2) return false;

            return tdElements[0].Text == name && tdElements[1].Text == director && tdElements[2].Text == year;
        }

        public void Dispose()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
