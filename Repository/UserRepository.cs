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
            if (item.Status == false)
            {
                context.Users.Remove(item);
                return context.SaveChanges();
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
            user.Role = context.Roles.Where(s => s.RoleId == user.RoleId).SingleOrDefault();
            context.Users.Add(new User()
            {
                UserName = user.UserName,
                RoleId = user.RoleId,
                Password = user.Password,
                CreatedDate = DateTime.Now,
                EditedDate = DateTime.Now,
                FullName = user.FullName,
                Phone = user.Phone,
                Email = user.Email,
                Address = user.Address,
                Department = user.Department,
                Position = user.Position,
                Avatar = user.Avatar,
                Note = user.Note,
                Status = user.Status
            });
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

        public IEnumerable<User> SearchUserInGroup(int id, string searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterGroup(GroupFilterModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FilterUserInGroup(GroupFilterModel model, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserOutGroup(int idgroup)
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

        public IEnumerable<User> FilterUser(UserFilterModel model)
        {
            var result = context.Users.ToList();
            if (model.Department != null)
            {
                result = result.Where(s => s.Department == model.Department).ToList();
            }
            if (model.Position != null)
            {
                result = result.Where(s => s.Position == model.Position).ToList();
            }
            return result;
        }
    }
}
