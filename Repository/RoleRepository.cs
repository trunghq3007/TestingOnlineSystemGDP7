using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : Interfaces.IGroupRepository<Role>
    {
        private DBEntityContext context;
        public RoleRepository(DBEntityContext context)
        {
            this.context = context;
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
            return context.Roles.ToList();
        }

        public Role GetById(int id)
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
    }
}
