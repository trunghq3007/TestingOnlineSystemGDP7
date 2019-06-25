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
    public class UserSevices : Interfaces.IGroupServices<User>
    {
        private IGroupRepository<User> userRepository;
        public UserSevices()
        {
            userRepository= new UserRepository(new DBEntityContext());
        }

        public bool CheckNameGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
        {
            return userRepository.CheckUserName(userName);
        }

        public int Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public int DeleteUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
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
            return userRepository.FilterUser(model);
        }

        public IEnumerable<User> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public User GetByUsername(string userName)
        {
            return userRepository.GetByUsername(userName);
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            return userRepository.GetDetailUser(id);
        }

        public List<int> GetListAction(string userName)
        {
            return userRepository.GetListAction(userName);
        }

        public string GetRoleName(int idUser)
        {
            return userRepository.GetRoleName(idUser);
        }

        public IEnumerable<User> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserOutGroup(int idgroup)
        {
            throw new NotImplementedException();
        }

        public int Insert(User user)
        {
            return userRepository.Insert(user);
        }

        public int InsertRoleAction(RoleAction roleAction)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(UserGroup userGroup)
        {
            throw new NotImplementedException();
        }

        public int InsertUserGroup(List<UserGroup> userGroup)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public int Login(LoginModel model, bool isLoginAdmin = false)
        {
            return userRepository.Login(model, isLoginAdmin);
        }

        public IEnumerable<User> Search(string searchString)
        {
            return userRepository.Search(searchString);
        }

        public IEnumerable<User> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(User user)
        {
            return userRepository.Update(user);
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }
    }
}