using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class DirectorRepository : IDirectorRepository
    {
        static List<Director> directors = new List<Director>();
        static int nextId = 1;

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Director GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Director> GetDirectors()
        {
            throw new NotImplementedException();
        }

        public int Save(Director director)
        {
            director.Id = nextId++;
            directors.Add(director);
            return director.Id;
        }

        public void Update(Director director)
        {
            throw new NotImplementedException();
        }
    }
}
