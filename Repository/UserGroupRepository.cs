using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
namespace Repository
{
    public class UserGroupRepository : Interfaces.IGroupRepository<User>
    {
        private DBEntityContext context;
        public UserGroupRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            var del = (from a in context.UserGroups
                where a.UserId == iduser && a.GroupId == idgroup
                select a).FirstOrDefault();
            context.UserGroups.Remove(del);
            return context.SaveChanges();
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
            var UserGroup = (from a in context.UserGroups
                join b in context.Users on a.UserId equals b.UserId
                where a.GroupId == id
                select b
                );
            return UserGroup.ToList();
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
