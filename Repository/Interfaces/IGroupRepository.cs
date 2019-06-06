using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
    public  interface IGroupRepository<T> where T:class
    {
        //GetAll method
        IEnumerable<T> GetAll();
        //Search by input searchString method
        IEnumerable<T> Search(string searchString);
        //Search User in Group method
        IEnumerable<T> SearchUserInGroup(int id,string searchString);
        //Filter method
        IEnumerable<T> Filter(T t);
        //Get User In Group method
        IEnumerable<T> GetUserInGroup(int id);
        //Insert method
        int Insert(T t);
        //Update method
        int Update(T t);
        //Delete method
        int Delete(int id);
        //Get by Id method
        T GetById(int id);
        //Filter group by model method
        IEnumerable<T> FilterGroup(GroupFilterModel model);
        //Filter user in group by model method
        IEnumerable<T> FilterUserInGroup(GroupFilterModel model,int id);
        //Get user not add to group method
        IEnumerable<T> GetUserOutGroup(int idgroup);
        //Add user to group
        int InsertUserGroup(int iduser, int idgroup);
        //Delete user in group
        int DeleteUserGroup(int iduser, int idgroup);
        //Filter user by input
        IEnumerable<T> FilterUser(UserFilterModel model);
        //Login method
        bool Login(string userName, string passWord);
        //Get by username
        T GetByUsername(string userName);
        //Update group
        int Update(int id, string groupname);
        //Get Detail user
        List<UserDetail> GetDetailUser(int id);
        bool CheckNameGroup(string groupName);
    }
}
