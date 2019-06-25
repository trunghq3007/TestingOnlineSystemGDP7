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
            return roleRepository.Delete(id);
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

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
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

        public List<int> GetListAction(string userName)
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

        public int Insert(Role t)
        {
            return roleRepository.Insert(t);
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

        public Role Login(string userName, string passWord, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        public int Login(LoginModel model, bool isLoginAdmin = false)
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
