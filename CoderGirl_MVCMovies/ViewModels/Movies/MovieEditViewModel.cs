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
        public List<int> DirectorIds { get; set; }
        public List<Director> Directors { get; set; }
        public int Year { get; set; }

        public MovieEditViewModel() { }

        public MovieEditViewModel(int id, MoviesDbContext context)
        {
            Movie movie=context.Movies.Find(id);
            this.DirectorIds = movie.DirectorMovies.Select(dm => dm.DirectorId).ToList();
            this.Year = movie.Year;
            this.Name = movie.Name;
            this.Directors = context.Directors.ToList();
        }

        public void Persist(int id, MoviesDbContext context)
        {
            Models.Movie movie = new Models.Movie
            {
                Id = id,
                Name = this.Name,
                Year = this.Year
            };
            context.Add(movie);
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

