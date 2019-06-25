using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;

namespace Services
{
    public class RoleActionServices : Interfaces.IGroupServices<Model.Action>
    {
        private IGroupRepository<Model.Action> roleActionRepository;

        public RoleActionServices()
        {
            roleActionRepository = new RoleActionRepository(new DBEntityContext());
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

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
        {
            return roleActionRepository.GetActionInRole(roleId);
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

        public Model.Action Login(string userName, string passWord, bool rememberMe)
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
            return roleActionRepository.DeleteActionRole(idAction, idRole);
        }
        public int InsertRoleAction(RoleAction roleAction)
        {
            return roleActionRepository.InsertRoleAction(roleAction);
        }
        public IEnumerable<Model.Action> GetActionOutRole(int idRole)
        {
            return roleActionRepository.GetActionOutRole(idRole);
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
