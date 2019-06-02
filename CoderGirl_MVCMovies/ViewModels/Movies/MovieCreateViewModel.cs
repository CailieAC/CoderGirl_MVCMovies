using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieCreateViewModel
    {
        public static MovieCreateViewModel GetMovieCreateViewModel()
        {
            List<Director> directors = RepositoryFactory.GetDirectorRepository()
                .GetModels()
                .Cast<Director>()
                .ToList();
            return new MovieCreateViewModel(directors);
        }


        public string Name { get; set; }
        public int DirectorId { get; set; }
        public List<Director> Directors { get; set; }
        public int Year { get; set; }
        
        private MovieCreateViewModel(List<Director> directors)
        {
            this.Directors = directors;
        }

        public void Persist()
        {
            Models.Movie movie = new Models.Movie
            {
                Name = this.Name,
                DirectorId = this.DirectorId,
                Year = this.Year
            };
            RepositoryFactory.GetMovieRepository().Save(movie);
        }
    }
}
