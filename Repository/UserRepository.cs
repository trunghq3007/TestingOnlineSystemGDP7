using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using System.Data.Entity;

namespace Repository
{
    public class UserRepository : Interfaces.IGroupRepository<User>,IDisposable
    {
        private DBEntityContext context;
        public UserRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            var item = context.Users.Where(s => s.UserId == id).SingleOrDefault();
            if (item != null)
            {
                context.Users.Remove(item);
                context.SaveChanges();

            }
            return 0;
        }

        public IEnumerable<User> Filter(User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.Where(s => s.UserId == id).SingleOrDefault();
        }

        public IEnumerable<User> GetUserInGroup(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(User user)
        {
            context.Users.Add(user);
            return context.SaveChanges();
        }

        public IEnumerable<User> Search(string searchString)
        {
            return context.Users.Where(s => s.FullName.Contains(searchString));
        }

        public int Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            return context.SaveChanges();
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
    }
}
