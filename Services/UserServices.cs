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
            userRepository = new UserRepository(new DBEntityContext());
        }
        public int Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public IEnumerable<User> Filter(User t)
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

        public int Insert(User user)
        {
            return userRepository.Insert(user);
        }

        public IEnumerable<User> Search(string searchString)
        {
            return userRepository.Search(searchString);
        }

        public int Update(User user)
        {
            return userRepository.Update(user);
        }
    }
}