using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.ViewModels.Directors
{
    public class DirectorCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public void Persist(MoviesDbContext context)
        {
            Director director = new Director
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                BirthDate = this.BirthDate,
                Nationality = this.Nationality
            };
            context.Add(director);
            context.SaveChanges();
        }
    }
}
