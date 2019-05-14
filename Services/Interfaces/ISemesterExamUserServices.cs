using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface ISemesterExamUserServices<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        int Insert(T t);

        int Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetCandidatesOfASemester(int id);
    }
}
