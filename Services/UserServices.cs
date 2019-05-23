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

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
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

        public int InsertUserGroup(int iduser, int idgroup)
        {
            throw new NotImplementedException();
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
    }
}