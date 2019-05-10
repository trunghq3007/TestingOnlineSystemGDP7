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
    public class QuestionServices : Interfaces.IServices<Question>
    {
        private IRepository<Question> repository;

        public QuestionServices()
        {
            repository = new QuestionRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(Question t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            return repository.GetAll();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Question t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Question t)
        {
            throw new NotImplementedException();
        }
    }
}
