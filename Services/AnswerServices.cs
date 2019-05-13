using DataAccessLayer;
using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Services
{
    public class AnswerServices : Interfaces.IServices<Answer>
    {
        private IRepository<Answer> repository;

        public AnswerServices()
        {
            repository = new AnswerRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public IEnumerable<Answer> Filter(Answer t)
        {
            return repository.Filter(t);
        }

        public IEnumerable<Answer> GetAll()
        {
            return repository.GetAll();
        }

        public Answer GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Answer t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<Answer> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public int Update(Answer t)
        {
            return repository.Update(t);
        }

        //public IEnumerable<Answer> GetAll()
    }
}
