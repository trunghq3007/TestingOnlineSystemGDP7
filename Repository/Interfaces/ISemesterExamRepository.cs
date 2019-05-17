using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ISemesterExamRepository<T>
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        int Insert(T t);
        int Update(T t);
        int Delete(int id);
        //T GetById(int id);
        Model.ViewModel.SemesterDetail GetById(int id);
        Model.ViewModel.ReportSemester Report(int id);
        int Update(T T, int id);


    }
}
