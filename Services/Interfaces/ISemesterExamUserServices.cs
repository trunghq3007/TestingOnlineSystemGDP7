using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    interface ISemesterExamUserServices<T>
    {
        //-------------------------------------Insert------------------------------------------------
        int InsertCandidates(int userid, int semesterid);
        //-------------------------------------Insert------------------------------------------------


        //-----------------------------------Compare Semesterid-----------------------------------------
        IEnumerable<T> GetUserOutGroup(int idgroup);
        //-----------------------------------Compare Semesterid-----------------------------------------


        IEnumerable<T> GetAll();

        int Insert(T t);

        IEnumerable<Model.ViewModel.Candidates> Search(string searchString, int id, int type);

        //int Delete(int id);
        //int DeleteUserInSemester(int userId, int semesterId);
        //int DeleteAllUserInSemester(int semesterId);
        T GetById(int id);
        IEnumerable<T> GetCandidatesOfASemester(int id);
        IEnumerable<SemesterExam_User> Filter(Candidates t);

        int DeleteCandidates(int userId, int semesterId);

        //Compare User - SemesterId
        IEnumerable<User> GetUserOutSemester(int semesterid);
    }
}