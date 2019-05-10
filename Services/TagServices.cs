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
    public class TagServices : Interfaces.IServices<Tag>
    {
        private IRepository<Tag> tagRepository;

        public TagServices()
        {
            tagRepository = new TagRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> Filter(Tag t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            return tagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Tag t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Tag t)
        {
            throw new NotImplementedException();
        }
    }
}
