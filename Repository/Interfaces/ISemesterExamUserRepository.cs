using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ISemesterExamUserRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search(string searchString);
        int Insert(T t);
        
        int Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetCandidatesOfASemester(int id);
      List<Model.ViewModel.Candidates>   candidates(int id);

        IEnumerable<Model.SemesterExam_User> Filter(Model.ViewModel.Candidates t);
        //IEnumerable<T> Search(string searchString);

        IEnumerable<Model.ViewModel.Candidates> Search(string searchString, int id, int type);


        
        int DeleteUserInSemester(int userId, int semesterId);

        int DeleteAllUserInSemester(int semesterId);

        
        


    }
}
