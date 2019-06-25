using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
    public class GroupRepository : Interfaces.IGroupRepository<Group>, IDisposable
    {
        private DBEntityContext context;

        public GroupRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            var group = context.Groups.Find(id);
            context.Groups.Remove(group);           
            return context.SaveChanges();
        }

        public IEnumerable<Group> Filter(Group t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            return context.Groups.ToList();
        }

        public Group GetById(int id)
        {
            return context.Groups.Where(s => s.GroupId == id).SingleOrDefault();
        }

        public int Insert(Group t)
        {
            context.Groups.Add(new Group()
            {
                CreatedDate =DateTime.Now,
                GroupName = t.GroupName,
                Creator = t.Creator,
                Description = t.Description
            });
            return context.SaveChanges();
        }


        public IEnumerable<Group> Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return context.Groups.Where(s => s.GroupName.Contains(searchString));
            }

            return context.Groups.ToList();
        }

        public int Update(Group t)
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

        public IEnumerable<Group> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FilterGroup(GroupFilterModel model)
        {
            var result = context.Groups.ToList();
            if (model.GroupId > 0)
            {
                result = result.Where(x => x.GroupId == model.GroupId).ToList();
            }

            if (model.StartDate != null)
            {
                result = result.Where(x => x.CreatedDate >= model.StartDate).ToList();
            }

            if (model.EndDate != null)
            {
                result = result.Where(x => x.CreatedDate <= model.EndDate).ToList();
            }
            return result;
        }

        public IEnumerable<Group> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public Group GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }
        public int Update(int id, string groupname)
        {
            var group = context.Groups.Where(s => s.GroupId == id).SingleOrDefault();
            group.EditedDate=DateTime.Now;
            group.GroupName = groupname;
            context.Entry(group).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckNameGroup(string groupName)
        {
            var check = context.Groups.Where(x => x.GroupName == groupName).Count() > 0;
            return check;
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

        public IEnumerable<Group> GetAction(string userName)
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

        public IEnumerable<Group> GetActionOutRole(int idRole)
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
