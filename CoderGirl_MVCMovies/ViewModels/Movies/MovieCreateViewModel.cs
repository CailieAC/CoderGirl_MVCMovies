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
    public class MovieCreateViewModel
    {
        private readonly RepositoryFactory repositoryFactory;

        public string Name { get; set; }
        public int DirectorId { get; set; }
        public IEnumerable<SelectListItem> Directors { get; set; }
        public int Year { get; set; }

        public MovieCreateViewModel() { }

        public MovieCreateViewModel(RepositoryFactory repositoryFactory)
        {
            this.Directors = GetDirectorList(repositoryFactory);
        }

        public void Persist(RepositoryFactory repositoryFactory)
        {
            Models.Movie movie = new Models.Movie
            {
                Name = this.Name,
                DirectorId = this.DirectorId,
                Year = this.Year
            };
            repositoryFactory.GetMovieRepository().Save(movie);
        }

        public void ResetDirectorList(RepositoryFactory repositoryFactory)
        {
            this.GetDirectorList(repositoryFactory);
        }

        private IEnumerable<SelectListItem> GetDirectorList(RepositoryFactory repositoryFactory)
        {
            return repositoryFactory.GetDirectorRepository()
                .GetModels()
                .Select(d => new SelectListItem(d.FullName, d.Id.ToString(), d.Id == this.DirectorId));
            //return new SelectList(directors, "Id", "FullName", this.DirectorId);
        }
    }
}
