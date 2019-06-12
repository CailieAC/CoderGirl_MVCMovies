using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movie
{
    public class MovieEditViewModel
    {
        /*
        public static MovieEditViewModel GetModel(int id)
        {
            Models.Movie movie = (Models.Movie)RepositoryFactory.GetMovieRepository().GetById(id);
            return new MovieEditViewModel
            {
                Name = movie.Name,
                DirectorName = movie.DirectorName,
                Year = movie.Year,

            };
        }
        */

        public static MovieEditViewModel GetModel(int id)
        {
            MovieEditViewModel viewModel = new MovieEditViewModel();
            List<Director> directors = RepositoryFactory.GetDirectorRepository()
                .GetModels()
                .Cast<Director>()
                .ToList();
            Models.Movie movie = (Models.Movie)RepositoryFactory.GetMovieRepository().GetById(id);
            viewModel.Directors = directors;
            viewModel.Name = movie.Name;
            viewModel.DirectorName = movie.DirectorName;
            viewModel.Year = movie.Year;
            viewModel.DirectorId = movie.DirectorId;

            return viewModel;
        }

        public string Name { get; set; }
        public string DirectorName { get; set; }
        public List<Director> Directors { get; set; }
        public int DirectorId { get; set; }
        public int Year { get; set; }

        public void Persist(int id)
        {
            Models.Movie movie = new Models.Movie
            {
                Id = id,
                Name = this.Name,
                DirectorName = this.DirectorName,
                Year = this.Year
            };
            RepositoryFactory.GetMovieRepository().Update(movie);
        }

    }
}
