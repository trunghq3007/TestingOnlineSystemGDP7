using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface ISemesterExamServices<T>
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        T GetById(int id);
        Model.ViewModel.ReportSemester Report(int id);
        IEnumerable<T> Filter(T t);
    }
}
