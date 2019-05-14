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
    public class QuestionServices : Interfaces.IQuestionServices<Question>
    {
        private IQuestionRepository<Question> repository;

        public QuestionServices()
        {
            repository = new QuestionRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }
        public IEnumerable<Question> Filter(Question t)
        {
            return null;
        }
        public IEnumerable<Question> Filter(QuestionFillterModel t)
        {
            return repository.Filter(t);
        }

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            return repository.GetAll();
        }

        public Question GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Question t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<Question> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public int Update(Question t)
        {
            return repository.Update(t);
        }
    }
}
