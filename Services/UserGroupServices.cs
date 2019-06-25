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
            return usergroupRepository.DeleteUserGroup(iduser, idgroup);
        }

        public IEnumerable<User> Filter(User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterUser(UserFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterUserInGroup(GroupFilterModel model, int id)
        {
            return usergroupRepository.FilterUserInGroup(model, id);
        }

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
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

        public User GetByUsername(string userName)
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

        public IEnumerable<User> GetUserInGroup(int id)
        {
            return usergroupRepository.GetUserInGroup(id);
        }

        public IEnumerable<User> GetUserOutGroup(int idgroup)
        {
            return usergroupRepository.GetUserOutGroup(idgroup);
        }

        public int Insert(User t)
        {
            throw new NotImplementedException();
        }

        public int InsertRoleAction(RoleAction roleAction)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(List<UserGroup> userGroup)
        {
            return usergroupRepository.InsertUserGroup(userGroup);
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public User Login(string userName, string passWord, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        public int Login(LoginModel model, bool isLoginAdmin = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SearchUserInGroup(int id, string searchString)
        {
            return usergroupRepository.SearchUserInGroup(id, searchString);
        }

        public int Update(User t)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
    }
}
