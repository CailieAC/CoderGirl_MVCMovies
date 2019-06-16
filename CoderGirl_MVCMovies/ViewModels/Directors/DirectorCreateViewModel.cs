using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.ViewModels.Directors
{
    public class DirectorCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public void Persist()
        {
            Director director = new Director
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                BirthDate = this.BirthDate,
                Nationality = this.Nationality
            };
            RepositoryFactory.GetDirectorRepository().Save(director);
        }
    }
}
