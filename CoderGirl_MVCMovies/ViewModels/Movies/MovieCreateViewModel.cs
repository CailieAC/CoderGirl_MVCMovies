using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Movies
{
    public class MovieCreateViewModel
    {
        [Required(ErrorMessage = "You must enter a movie name!")]
        public string Name { get; set; }

        [Display(Name="Select Director(s)")]
        public List<int> DirectorIds { get; set; }
        public List<Director> Directors { get; set; }

        [Required]
        [Range(1887, 2019, ErrorMessage = "Impossible Year for any Movie")]
        [Display(Name="Year")]
        public int Year { get; set; }

        public MovieCreateViewModel() { }

        public MovieCreateViewModel(MoviesDbContext context)
        {
            this.Directors = context.Directors.ToList();
        }

        public void Persist(MoviesDbContext context)
        {
            Models.Movie movie = new Models.Movie
            {
                Name = this.Name,
                //DirectorId = this.DirectorId,
                Year = this.Year
            };
            context.Movies.Add(movie);
            List<DirectorMovie> directorMovies = CreateManyToManyRelationships(movie.Id);
            movie.DirectorMovies = directorMovies;
            context.SaveChanges();
        }

        private List<DirectorMovie> CreateManyToManyRelationships(int movieId)
        {
            return DirectorIds.Select(dirId => new DirectorMovie { MovieId = movieId, DirectorId = dirId }).ToList();
        }

        internal void ResetDirectorList(MoviesDbContext context)
        {
           this.Directors = context.Directors.ToList();
        }
    }
}
