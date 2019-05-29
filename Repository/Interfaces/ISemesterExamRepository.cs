using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;

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
        Model.ViewModel.ReportSemester Report(int id);
        Model.ViewModel.SemesterDetail GetById(int id);
        IEnumerable<Model.Test> GetTests(int id);
        IEnumerable<Model.Exam> GetExamsNotAdd(int id);
        int AddMany(int[] listId, int semesterId);
        IEnumerable<T> InputCode(string code);
        IEnumerable<Model.Exam> GetExams(int id);
        IEnumerable<T> GetByCandidateId(int candidateId);
        IEnumerable<T> Filter(T t);
        IEnumerable<Model.Exam> SearchExams(string examName, int id);


    }
}
