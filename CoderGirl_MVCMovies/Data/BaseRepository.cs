using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class BaseRepository<T> where T : IModel
    {
        protected List<T> models = new List<T>();
        protected int nextId = 1;

        public void Delete(int id)
        {
            models.RemoveAll(d => d.Id == id);
        }

        public virtual T GetById(int id)
        {
            return models.SingleOrDefault(d => d.Id == id);
        }

        public virtual List<T> GetModels()
        {
            return models;
        }

        public int Save(T model)
        {
            model.Id = nextId++;
            models.Add(model);
            return model.Id;
        }

        public void Update(T model)
        {
            this.Delete(model.Id);
            models.Add(model);
        }
    }
}
