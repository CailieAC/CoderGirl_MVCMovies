using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoderGirl_MVCMovies.Data;
using CoderGirl_MVCMovies.Models;

namespace CoderGirl_MVCMovies.ViewModels.Directors
{
    public class DirectorListItemViewModel
    {
        internal static List<DirectorListItemViewModel> GetDirectors(MoviesDbContext context)
        {
            return context.Directors
                .Select(d => GetListItem(d))
                .ToList();
        }

        private static DirectorListItemViewModel GetListItem(Director d)
        {
            return new DirectorListItemViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                BirthDate = d.BirthDate,
                Nationality = d.Nationality
            };
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
    }
}
