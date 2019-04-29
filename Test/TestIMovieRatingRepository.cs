using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoderGirl_MVCMovies.Data;
using Xunit;

namespace Test
{
    public class TestIMovieRatingRepository
    {
        private IMovieRatingRepository repository;

        public TestIMovieRatingRepository()
        {
            repository = RepositoryFactory.GetMovieRatingRepository();
        }

        [Fact]
        public void TestSave_ReturnsDistinctIds_GreaterThanZero()
        {
            List<int> ids = new List<int>();
            ids.Add(repository.SaveRating("movie1", 1));
            ids.Add(repository.SaveRating("movie2", 2));
            ids.Add(repository.SaveRating("movie3", 3));

            var distinctResults = ids.Distinct();
            Assert.Equal(3, distinctResults.Count());
            Assert.True(distinctResults.Min() > 0);
        }

        [Theory]
        [InlineData("movie3")]
        [InlineData("movie4")]
        public void TestGetMovieNameById_ReturnsMovieName_WhenGivenId(string movieName)
        {
            int id = repository.SaveRating(movieName, 1);

            var result = repository.GetMovieNameById(id);

            Assert.Equal(movieName, result);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void TestGetRatingById(int rating)
        {
            int id = repository.SaveRating("movie9", rating);

            var result = repository.GetRatingById(id);

            Assert.Equal(rating, result);
        }

        [Theory]
        [InlineData("movie11", new int[] { 1, 3, 5 })]
        [InlineData("movie12", new int[] { 3, 2, 5 })]
        public void TestGetAverageRatingByMovieName(string movieName, int[] ratings)
        {
            Array.ForEach(ratings, rating => repository.SaveRating(movieName, rating));
            var expected = Math.Round(ratings.Average(), 3);

            var result = Math.Round(repository.GetAverageRatingByMovieName(movieName), 3);

            Assert.Equal(expected, result);
        }
    }
}
