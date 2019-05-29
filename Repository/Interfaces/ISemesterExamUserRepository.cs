using Model;
using Model.ViewModel;
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
        //IEnumerable<T> Search(string searchString);
        int Insert(T t);
        IEnumerable<Model.ViewModel.Candidates> Search(string searchString, int id, int type);

        T GetById(int id);
        IEnumerable<T> GetCandidatesOfASemester(int id);
        List<Model.ViewModel.Candidates> candidates(int id);

        IEnumerable<SemesterExam_User> Filter(Candidates t);

        //Delete
        int DeleteCandidates(int userId, int semesterId);

        //Insert
        int InsertCandidates(int userid, int semesterid);

        //Compare User - SemesterId
        IEnumerable<User> GetUserOutSemester(int semesterid);

    }
}