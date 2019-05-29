using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using DataAccessLayer;

namespace Services
{
    public class RoleServices : Interfaces.IGroupServices<Role>
    {
        private IGroupRepository<Role> roleRepository;
        public RoleServices()
        {
            roleRepository = new RoleRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Filter(Role t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAll()
        {
            return roleRepository.GetAll();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Role GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int Insert(Role t)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public Role Login(string userName, string passWord, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Role t)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
    }
}
