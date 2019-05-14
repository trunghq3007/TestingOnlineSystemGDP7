using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;

namespace Services
{
    public class CategoryService : Interfaces.ICategoryServices<Category>
    {
        private ICategoryRepository<Category> repository;
        public CategoryService()
        {
            repository = new CategoryRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }

        public IEnumerable<Category> Filter(Category t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return repository.GetAll();
        }

        public Category GetById(int id)
        {
           return repository.GetById(id);
        }

        public int Insert(Category t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<Category> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public int Update(Category t)
        {
            return repository.Update(t);
        }
    }
}
