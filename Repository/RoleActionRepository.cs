using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Repository.Interfaces;

namespace Repository
{
    public class RoleActionRepository : IGroupRepository<Model.Action>, IDisposable
    {
        private DBEntityContext context;
        public RoleActionRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public bool CheckNameGroup(string groupName, int groupId)
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

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> Filter(Model.Action t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> GetAction(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
        {
            var result = context.Actions.Where(s => s.RoleActions.Where(x => x.RoleId == roleId).Count() > 0);
            //var result = (from a in context.Actions
            //    join r in context.RoleActions on a.ActionId equals r.ActionId
            //    where r.RoleId == roleId
            //    select a).ToList();
            return result;
        }

        public IEnumerable<Model.Action> GetAll()
        {
            throw new NotImplementedException();
        }

        public Model.Action GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Model.Action GetByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> GetListAction(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetRoleName(int idUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int Insert(Model.Action t)
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

        public int Login(LoginModel model, bool isLoginAdmin = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.Action t)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
        public int DeleteActionRole(int idAction, int idRole)
        {
            var del = (from a in context.RoleActions
                where a.ActionId == idAction && a.RoleId == idRole
                select a).FirstOrDefault();
            context.RoleActions.Remove(del);
            return context.SaveChanges();
        }
        public int InsertRoleAction(RoleAction roleAction)
        {
            RoleAction item = new RoleAction();
            item.ActionId = roleAction.ActionId;
            item.RoleId = roleAction.RoleId;
            item.IsTrue = true;
            context.RoleActions.Add(item);
            return context.SaveChanges();
        }
        public IEnumerable<Model.Action> GetActionOutRole(int idRole)
        {
            var actioninrole = context.Actions.Where(s => s.RoleActions.Where(x => x.RoleId == idRole).Count() > 0);
            var action = new List<Model.Action>(context.Actions);
            foreach (var item in actioninrole)
            {
                var t = action.Remove(item);
            }
            var remainaction = action.ToList();
            return remainaction;
        }

        public int InsertUserGroup(UserGroup userGroup)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(List<UserGroup> userGroup)
        {
            throw new NotImplementedException();
        }
    }
}
