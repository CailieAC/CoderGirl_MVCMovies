using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    // TODO: I have provided this for you. Use it or change it as you wish. This is merely a suggested pattern to follow
    public interface IMovieRatingRepository
    {
        int Save(MovieRating movieRating);

        List<MovieRating> GetMovieRatings();

        MovieRating GetById(int id);

        void Update(MovieRating movie);

        void Delete(int id);
    }
}
