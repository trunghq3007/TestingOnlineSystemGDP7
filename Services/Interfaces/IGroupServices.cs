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
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        T GetById(int id);
    }
}
