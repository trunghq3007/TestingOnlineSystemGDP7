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
    public class TagServices : Interfaces.ITagServices<Tag>
    {
        private ITagRepository<Tag> repository;

        public TagServices()
        {
            repository = new TagRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return repository.Delete(id);
        }
       
        public IEnumerable<Tag> GetAll()
        {
            return repository.GetAll();
        }

        public Tag GetById(int id)
        {
            return repository.GetById(id);
        }

        public int Insert(Tag t)
        {
            return repository.Insert(t);
        }

        public IEnumerable<Tag> Search(string searchString)
        {
            return repository.Search(searchString);
        }

        public int Update(Tag t)
        {
            return repository.Update(t);
        }
    }
}
