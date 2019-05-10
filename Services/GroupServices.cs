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
    public class GroupServices : Interfaces.IServices<Group>
    {
        private IRepository<Group> groupRepository;

        public GroupServices()
        {
            groupRepository = new GroupRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Filter(Group t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Group t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Group t)
        {
            throw new NotImplementedException();
        }
    }
}
