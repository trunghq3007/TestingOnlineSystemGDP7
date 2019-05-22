using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.Interfaces;
using Repository;
using DataAccessLayer;
using Model.ViewModel;

namespace Services
{
   public  class SemesterExamUserServices : Interfaces.ISemesterExamUserServices<SemesterExam_User>
    {
        private ISemesterExamUserRepository<SemesterExam_User> repository;

        public SemesterExamUserServices()
        {
            repository = new SemesterExamUserRepository( (new DBEntityContext()));
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> GetAll()
        {
            throw new NotImplementedException();
        }

        public SemesterExam_User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> GetCandidatesOfASemester(int id)
        {
            return repository.GetCandidatesOfASemester(id);
        }

        public int Insert(SemesterExam_User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> Search(string searchString)
        {
            throw new NotImplementedException();
        }
        public List<Model.ViewModel.Candidates> candidates(int id)
        {
            return repository.candidates(id);
        }
        public int DeleteUserInSemester(int userId, int semesterId)
        {
            return repository.DeleteUserInSemester(userId, semesterId);
        }

        
        public int DeleteAllUserInSemester(int semesterId)
        {
            return repository.DeleteAllUserInSemester(semesterId);
        }
        public IEnumerable<Candidates> Search(string searchString, int id, int type)
        {
            return repository.Search(searchString, id, type);
        }
        public IEnumerable<SemesterExam_User> Filter(Candidates t)
        {
            return repository.Filter(t);
        }

    }
}
