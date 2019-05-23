using CoderGirl_MVCMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoderGirl_MVCMovies.Data
{
    public interface IRepository
    {

        IModel GetById(int id);
        List<IModel> GetModels();

        //Save returns int id of save model
        int Save(IModel model);
        void Delete(int id);
        void Update(IModel model);
    }
}
