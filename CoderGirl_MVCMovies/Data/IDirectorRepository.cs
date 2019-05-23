using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public interface IDirectorRepository
    {
        //TODO could delete these three repositories, or test how you could integrate and still use them

        int Save(Director director);

        List<Director> GetDirectors();

        Director GetById(int id);

        void Update(Director director);

        void Delete(int id);
    }
}
