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
            if (!string.IsNullOrEmpty(searchString))
            {
                return context.Users.Where(s => s.FullName.Contains(searchString));
            }
            return context.Users.ToList();
        }

        public int Update(User user)
        {

            var currentUser = context.Users.Find(user.UserId);

            currentUser.EditedDate = DateTime.Now;

            currentUser.Email = user.Email;
            currentUser.Role = context.Roles.Find(user.RoleId);
            currentUser.RoleId = user.RoleId;
            currentUser.FullName = user.FullName;
            currentUser.Phone = user.Phone;
            currentUser.Address = user.Address;
            currentUser.Department = user.Department;
            currentUser.Position = user.Position;
            currentUser.Avatar = user.Avatar;
            currentUser.Status = user.Status;
            currentUser.Note = user.Note;


            context.Entry(currentUser).State = EntityState.Modified;
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

        public bool Login(string userName, string passWord)
        {
            var result = context.Users.Count(x => x.UserName == userName && x.Password == passWord);
            if (result > 0)
                return true;
            else return false;
        }

        public User GetByUsername(string userName)
        {
            return context.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public int Update(int id, string groupname)
        {
            throw new NotImplementedException();
        }

        public List<UserDetail> GetDetailUser(int id)
        {
            var listuserdetail =
            (
                from u in context.Users
                join ug in context.UserGroups on u.UserId equals ug.UserId
                join g in context.Groups on ug.GroupId equals g.GroupId
                where u.UserId == id
                select new
                {
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    Department = u.Department,
                    Position = u.Position,
                    GroupName = g.GroupName
                }

            );
            List<UserDetail> list = new List<UserDetail>();
            foreach (var item in listuserdetail)
            {
                UserDetail userDetail = new UserDetail();
                userDetail.FullName = item.FullName;
                userDetail.UserName = item.UserName;
                userDetail.Email = item.Email;
                userDetail.Department = item.Department;
                userDetail.Position = item.Position;
                userDetail.GroupName = item.GroupName;
                list.Add(userDetail);
            }
            return list;
        }

        public bool CheckNameGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserName(string userName)
        {
            var check = context.Users.Where(s => s.UserName == userName).Count() > 0;
            return check;
        }

        public string GetRoleName(int idUser)
        {
            string roleName = "";
            var user = context.Users.SingleOrDefault(s => s.UserId == idUser);
            if (user != null)
            {
                roleName = user.Role.RoleName;
            }
            return roleName;
        }

        public IEnumerable<Model.Action> GetActionInRole(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
