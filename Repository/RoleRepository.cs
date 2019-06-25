using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : Interfaces.IGroupRepository<Role>, IDisposable
    {
        private DBEntityContext context;
        public RoleRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Insert(Role t)
        {
            context.Roles.Add(new Role() {
                RoleName = t.RoleName,
                Description = t.Description
            });
            return context.SaveChanges();
        }
        public int Update(Role t)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            return context.SaveChanges();
        }

        public bool CheckNameGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
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

        public Role GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public string GetRoleName(int idUser)
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


        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
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

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public int Login(LoginModel model, bool isLoginAdmin = false)
        {
            throw new NotImplementedException();
        }

        public List<int> GetListAction(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAction(string userName)
        {
            throw new NotImplementedException();
        }

        public int DeleteActionRole(int idAction, int idRole)
        {
            throw new NotImplementedException();
        }

        public int InsertRoleAction(int idAction, int idRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetActionOutRole(int idRole)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(UserGroup userGroup)
        {
            throw new NotImplementedException();
        }

        public int InsertRoleAction(RoleAction roleAction)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(List<UserGroup> userGroup)
        {
            throw new NotImplementedException();
        }
    }
}
