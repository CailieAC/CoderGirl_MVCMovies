using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRepository : IMovieRespository
    {
        static List<Movie> movies = new List<Movie>();
        static int nextId = 1;
        static IMovieRatingRepository ratingRepository = RepositoryFactory.GetMovieRatingRepository();

        public void Delete(int id)
        {
            movies.RemoveAll(m => m.Id == id);
        }

        public Movie GetById(int id)
        {
            Movie movie = movies.SingleOrDefault(m => m.Id == id);
            movie = SetMovieRatings(movie);
            return movie;
        }

        public List<Movie> GetMovies()
        {
            return movies.Select(movie => SetMovieRatings(movie)).ToList();
        }

        public int Save(Movie movie)
        {
            movie.Id = nextId++;
            movies.Add(movie);
            return movie.Id;
        }

        public void Update(Movie movie)
        {
            this.Delete(movie.Id);
            movies.Add(movie);
        }

        private Movie SetMovieRatings(Movie movie)
        {
            List<int> ratings = ratingRepository.GetMovieRatings()
                                                .Where(rating => rating.MovieId == movie.Id)
                                                .Select(rating => rating.Rating)
                                                .ToList();
            movie.Ratings = ratings;
            return movie;
        }
    }
}
