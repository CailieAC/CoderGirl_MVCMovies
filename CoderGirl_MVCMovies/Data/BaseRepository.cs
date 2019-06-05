using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public class BaseRepository : IRepository
    {
        protected List<IModel> models = new List<IModel>();
        protected int nextId = 1;

        public void Delete(int id)
        {
            models.RemoveAll(d => d.Id == id);
        }

        public virtual IModel GetById(int id)
        {
            return models.SingleOrDefault(d => d.Id == id);
        }

        public virtual List<IModel> GetModels()
        {
            return models;
        }

        public int Save(IModel model)
        {
            model.Id = nextId++;
            models.Add(model);
            return model.Id;
        }

        public void Update(IModel model)
        {
            this.Delete(model.Id);
            models.Add(model);
        }
    }
}
