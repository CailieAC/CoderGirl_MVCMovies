using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieEditViewModel
    {
        public string Name { get; set; }
        public int DirectorId { get; set; }
        public SelectList Directors { get; set; }
        public int Year { get; set; }

        public MovieEditViewModel() { }

        public MovieEditViewModel(RepositoryFactory repositoryFactory)
        {
            this.Directors = GetDirectorList(repositoryFactory);
        }

        public MovieEditViewModel(int id)
        {
            Movie movie=repositoryFactory.GetMovieRepository().GetById(id);
            //this.DirectorId = movie.Director.Id;
            this.Year = movie.Year;
            this.Name = movie.Name;
        }

        public void Persist(int id, RepositoryFactory repositoryFactory)
        {
            Models.Movie movie = new Models.Movie
            {
                Id = id,
                Name = this.Name,
                //DirectorId = this.DirectorId,
                Year = this.Year
            };
            repositoryFactory.GetMovieRepository().Save(movie);
        }

        public void ResetDirectorList(RepositoryFactory repositoryFactory)
        {
            GetDirectorList(repositoryFactory);
        }

        private SelectList GetDirectorList(RepositoryFactory repositoryFactory)
        {
            var directors = repositoryFactory.GetDirectorRepository()
                .GetModels();
            return new SelectList(directors, "Id", "FullName", this.DirectorId);
        }
    }
}

