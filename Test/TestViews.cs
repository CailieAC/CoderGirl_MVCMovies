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
using System.Collections.ObjectModel;

namespace Test
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class TestViews : IDisposable
    {
        private ChromeDriver driver;
        private const string BASE_URL = "http://localhost:59471";


        public TestViews()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Theory, TestPriority(-1)]
        [InlineData("George", "Lucas", "11111111", "USA")]
        [InlineData("Stephen", "Spielberg", "12122019", "German")]
        public void TestCreateDirector(string first, string last, string birth, string nation)
        {
            //navigate to director index and click link Add Director 
            driver.Url = BASE_URL + "/Director";
            driver.FindElementById("add-new-director").Click();
            VerifyRoute("/Director/Create");

            //enter values and click submit
            driver.FindElementByName("FirstName").SendKeys(first);
            driver.FindElementByName("LastName").SendKeys(last);
            driver.FindElementByName("BirthDate").SendKeys(birth);
            driver.FindElementByName("Nationality").SendKeys(nation);
            var form = driver.FindElementByTagName("form");
            form.FindElement(By.TagName("button")).Click();

            //verify redirected to /Director
            VerifyRoute("/Director");
        }

        [Theory, TestPriority(0)]
        [InlineData("Lucas, George", "11/11/1111", "USA")]
        [InlineData("Spielberg, Stephen", "12/12/2019", "German")]
        public void TestDirectorList(string fullName, string birth, string nation)
        {
            //navigate to /Director and verify info
            driver.Url = BASE_URL + "/Director";

            //get table rows
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //Verify the first row has proper headers
            Assert.Equal("Name", headers[0].Text);
            Assert.Equal("Birth Date", headers[1].Text);
            Assert.Equal("Nationality", headers[2].Text);

            //Verify a row contains expected movie/rating combo
            Assert.Contains(rows, row => DirectorRowMatches(row, fullName, birth, nation));
        }

        [Theory, TestPriority(1)]
        [InlineData("Star Wars", "Lucas, George", "1976")]
        [InlineData("Princess Bride", "Spielberg, Stephen", "1987")]
        public void TestCreateMovie(string name, string director, string year)
        {
            //add movies to data
            driver.Url = BASE_URL + "/movie/create";
            driver.FindElementByName("Name").SendKeys(name);
            var directorSelect = new SelectElement(driver.FindElementByName("DirectorId"));
            directorSelect.SelectByText(director);
            driver.FindElementByName("Year").SendKeys(year);
            var movieForm = driver.FindElementByTagName("form");
            var movieSubmit = movieForm.FindElement(By.TagName("button"));
            movieSubmit.Click();

            //verify it redirects to Index
            Assert.Equal(Uri.EscapeUriString(BASE_URL + $"/movie"), driver.Url, true);
        }

        [Theory, TestPriority(1)]
        [InlineData("Star Wars", "Lucas, George", "1976")]
        [InlineData("Princess Bride", "Spielberg, Stephen", "1987")]
        public void TestMovieList(string name, string director, string year)
        {
            //navigate to page and get table rows
            driver.Url = BASE_URL + "/Movie";
            var rows = driver.FindElementsByTagName("tr");
            var headers = rows[0].FindElements(By.TagName("th"));

            //Verify the first row has proper headers
            Assert.Equal("Movie Name", headers[0].Text);
            Assert.Equal("Director", headers[1].Text);
            Assert.Equal("Year", headers[2].Text);
            Assert.Equal("Average Rating", headers[3].Text);
            Assert.Equal("Number of Ratings", headers[4].Text);

            //Verify a row contains expected movie
            Assert.Contains(rows, row => MovieRowMatches(row, name, director, year, "none", "0"));
        }

        [Theory, TestPriority(3)]
        [InlineData("Star Wars", "5", 5, 1)]
        [InlineData("Princess Bride", "4", 4, 1)]
        [InlineData("Star Wars", "3", 4, 2)]
        [InlineData("Princess Bride", "1", 2.5, 2)]
        public void TestCreateMovieRating(string name, string rating, double average, int count)
        {
            //navigate to add movie list page and click Rate link
            driver.Url = BASE_URL + "/Movie";
            var rows = driver.FindElementsByTagName("tr");
            var testRow = GetRowByMatchingText(rows, name, 0);
            string movieId = testRow.GetAttribute("id");
            GetRateLink(testRow).Click();

            //Verify it went to /MovieRating/Create/{movieId}
            VerifyRoute("/MovieRating/Create?movieId=" + movieId);

            //make selections for input and submit
            var form = driver.FindElementByTagName("form");
            var submit = form.FindElement(By.TagName("button"));
            Assert.Equal("submit", submit.GetAttribute("type"));
            Assert.Equal("Rate Movie", submit.Text);
            var ratingSelectInput = new SelectElement(driver.FindElementByName("Rating"));
            var movieInput = driver.FindElementByName("MovieName");
            Assert.Equal(name, movieInput.GetAttribute("value"));
            movieInput.SendKeys("badName");
            ratingSelectInput.SelectByText(rating);
            submit.Click();

            //verify it redirects to /Movie/Index and average/count values are correct
            Assert.Equal(Uri.EscapeUriString(BASE_URL + $"/Movie"), driver.Url, true);
            rows = driver.FindElementsByTagName("tr");
            testRow = GetRowByMatchingText(rows, name, 0);
            Assert.Equal(average, Convert.ToDouble(testRow.FindElements(By.TagName("td"))[3].Text));
            Assert.Equal(count, Convert.ToInt32(testRow.FindElements(By.TagName("td"))[4].Text));
        }

        private IWebElement GetRowByMatchingText(ReadOnlyCollection<IWebElement> rows, string text, int columnIndex)
        {
            return rows.Skip(1).SingleOrDefault(row => row.FindElements(By.TagName("td"))[columnIndex].Text == text);
        }

        private static IWebElement GetEditLink(IWebElement testRow)
        {
            return testRow.FindElement(By.LinkText("Edit"));
        }

        private static IWebElement GetDeleteLink(IWebElement testRow)
        {
            return testRow.FindElement(By.LinkText("Delete"));
        }

        private static IWebElement GetRateLink(IWebElement testRow)
        {
            return testRow.FindElement(By.LinkText("Rate"));
        }

        private string GetRouteValueForLink(IWebElement editLink)
        {
            var action = editLink.GetAttribute("href").ToString();
            return action.Substring(action.LastIndexOf("/") + 1);
        }

        private bool DirectorRowMatches(IWebElement row, string name, string birth, string nation)
        {
            var tdElements = row.FindElements(By.TagName("td"));
            if (tdElements.Count < 2) return false;

            return tdElements[0].Text == name && MatchByDateTimeOrString(tdElements[1].Text, birth) && tdElements[2].Text == nation;
        }

        private bool MatchByDateTimeOrString(string text, string birth)
        {
            return DateTime.Parse(text) == DateTime.Parse(birth)
                || text == birth;
        }

        private bool MovieRatingRowMatches(IWebElement row, string name, string rating)
        {
            var tdElements = row.FindElements(By.TagName("td"));
            if (tdElements.Count < 2) return false;

            return tdElements[0].Text == name && tdElements[1].Text == rating;
        }

        private bool MovieRowMatches(IWebElement row, string name, string director, string year, string averageRating, string ratingNumber)
        {
            var tdElements = row.FindElements(By.TagName("td"));
            if (tdElements.Count < 2) return false;

            return tdElements[0].Text == name && tdElements[1].Text == director && tdElements[2].Text == year;
        }

        private void VerifyRoute(string route)
        {
            Assert.Equal(Uri.EscapeUriString(BASE_URL + route), driver.Url, true);
        }

        public void Dispose()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
