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
			directors.RemoveAll(m => m.Id == id);
		}

        public Director GetById(int id)
        {
			Director director = directors.SingleOrDefault(m => m.Id == id);			
			return director;
		}

        public List<Director> GetDirectors()
        {
			return directors;
		}

        public int Save(Director director)
        {
            director.Id = nextId++;
            directors.Add(director);
            return director.Id;
        }

        public void Update(Director director)
        {
			this.Delete(director.Id);
			directors.Add(director);
		}
    }
}