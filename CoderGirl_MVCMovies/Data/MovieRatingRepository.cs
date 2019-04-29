using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class MovieRatingRepository : IMovieRatingRepository
    {
        public static List<Movie> Movies = new List<Movie>();

        public decimal GetAverageRatingByMovieName(string movieName)
        {
            // Given a movie name, returns the average rating of the movie.
            // If there are no ratings for the movie, returns an empty list.
            bool inList = false;
            
            foreach (Movie movie in Movies)
            {
                if (movie.Name == movieName)
                {
                    inList = true;

                }
            }

            if (inList == false)
            {
                decimal emptyList = 0;
                return emptyList;
            }

            decimal average = 0;
            decimal total = 0;
            decimal count = 0;
            foreach (Movie movie in Movies)
            {
                foreach (var rating in movie.Rating)
                {
                    total += rating;
                    count++;
                }
            }
            average = total / count;

            return average;
        }

        public List<int> GetIds()
        {
            return Movies.Select(p => p.Id).ToList();
        }

        public string GetMovieNameById(int id)
        {
            return Movies[id - 1].Name;
        }

        public int GetRatingById(int id)
        {
            return Movies[id - 1].Rating[id];
        }

        public int SaveRating(string movieName, int rating)
        {
            // Given a movieName and rating, saves the rating and returns a unique id > 0.
            // If the movie name and/or rating are null or empty, nothing should be saved and it should return 0
            if (String.IsNullOrEmpty(movieName) || rating == 0)
            {
                return 0;
            }
            Movie movie = new Movie();
            movie.Name = movieName;
            movie.Rating.Add(rating);
            movie.Id = Movies.Count + 1;
            Movies.Add(movie);
            return movie.Id;
        }    
    }
}
