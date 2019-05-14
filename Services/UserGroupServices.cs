using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using DataAccessLayer;
using Model;
using Repository.Interfaces;

namespace Services
{
    public class UserGroupServices : Interfaces.IGroupServices<User>
    {
        private IGroupRepository<User> usergroupRepository;

        public UserGroupServices()
        {
           usergroupRepository = new UserGroupRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            return usergroupRepository.DeleteUserGroup(iduser, idgroup);
        }

        public IEnumerable<User> Filter(User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserInGroup(int id)
        {
            return usergroupRepository.GetUserInGroup(id);
        }

        public int Insert(User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(User t)
        {
            throw new NotImplementedException();
        }
    }
}
