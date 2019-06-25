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
    public class UserGroupRepository : Interfaces.IGroupRepository<User>, IDisposable
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
            var result = context.Users.Where(s => s.UserGroups.Where(x => x.GroupId == id).Count() > 0);
            return result;
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

        public IEnumerable<User> SearchUserInGroup(int id, string searchString)
        {
            var result = context.Users.Where(s => s.UserGroups.Where(x => x.GroupId == id).Count() > 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                return result.Where(s => s.FullName.Contains(searchString));
            }            
            return context.Users.Where(s => s.UserGroups.Where(x => x.GroupId == id).Count() > 0);
        }

        public IEnumerable<User> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterUserInGroup(GroupFilterModel model, int id)
        {
            var result = context.Users.Where(s => s.UserGroups.Where(x => x.GroupId == id).Count() > 0);
            //var result2=context.UserGroups.Where(s=>s.CreatedOn==model.AddedEndDate)
            if (model.Position != null)
            {
                result = result.Where(s => s.Position == model.Position);
            }
            if (model.Department != null)
            {
                result = result.Where(s => s.Department == model.Department);
            }
            return result;
        }

        public IEnumerable<User> GetUserOutGroup(int idgroup)
        {
            var userInGroup = context.Users.Where(s => s.UserGroups.Where(x => x.GroupId == idgroup).Count() > 0);
            var user = new List<User>(context.Users);
            foreach (var item in userInGroup)
            {
                var t = user.Remove(item);
            }
            var userss = user.ToList();
            return user;
        }

        public int InsertUserGroup(List<UserGroup>  userGroup)
        {
            foreach (var item in userGroup)
            {
                UserGroup userGroups = new UserGroup();
                userGroups.UserId = item.UserId;
                userGroups.GroupId = item.GroupId;
                userGroups.CreatedOn = DateTime.Now;
                context.UserGroups.Add(item);
            }
           
           
            return context.SaveChanges();
        }

        public IEnumerable<User> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckNameGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetRoleName(int idUser)
        {
            throw new NotImplementedException();
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

        public IEnumerable<User> GetAction(string userName)
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

        public IEnumerable<User> GetActionOutRole(int idRole)
        {
            throw new NotImplementedException();
        }

        public int InsertRoleAction(RoleAction roleAction)
        {
            throw new NotImplementedException();
        }
    }
}
