using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGroupServices<T> where T:class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        IEnumerable<T> Filter(T t);
        IEnumerable<T> GetUserInGroup(int id);
        IEnumerable<T> SearchUserInGroup(int id, string searchString);
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        T GetById(int id);
        IEnumerable<T> FilterGroup(GroupFilterModel model);
        IEnumerable<T> FilterUserInGroup(GroupFilterModel model, int id);
        IEnumerable<T> GetUserOutGroup(int idgroup);
        int InsertUserGroup(int iduser, int idgroup);
        int DeleteUserGroup(int iduser, int idgroup);
        IEnumerable<T> FilterUser(UserFilterModel model);
    }
}
