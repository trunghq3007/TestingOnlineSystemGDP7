using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;

namespace Repository
{
   public class SemesterExamUserRepository : Repository.Interfaces.ISemesterExamUserRepository<SemesterExam_User>
    {
        private DBEntityContext context;

        public SemesterExamUserRepository(DBEntityContext context)
        {
            this.context = context;
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

        public int Insert(SemesterExam_User t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam_User> Search(string searchString)
        {
            throw new NotImplementedException();
        }
         private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<SemesterExam_User> GetCandidatesOfASemester(int id)
        {
            //List<User> users = context.Users.ToList();
            //List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            return context.SemesterExamUsers.Where(S => S.SemesterExam.ID == id && S.Type == 2).ToList();
           
        }

        public List<Model.ViewModel.Candidates> candidates(int id)
        {
            User user = context.Users.Find(id);
         List<SemesterExam_User>    semesterExam_Users =
            context.SemesterExamUsers.Where(SU => SU.SemesterExam.ID == id && SU.Type == 2).ToList(); ;
            var query = from U in context.Users
                        join SEU in context.SemesterExamUsers on U.UserId equals SEU.User.UserId
                        select U;

            int count = query.ToList().Count;
            List<Candidates> candidates = new List<Candidates>();

            //User user = new User();
            //user.SemesterExam_Users.Where(S => S.ID == 1).ToList();
            //SemesterExam semesterExam= new SemesterExam();
            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            List<Candidates> abc = new List<Candidates>();
            
            foreach(SemesterExam_User item in semesterExam_Users)
            {
                Candidates I = new Candidates();
                I.UserId = item.User.UserId;
                I.UserName = item.User.UserName;
                I.FullName = item.User.FullName;
                I.Email = item.User.Email;
                I.Department = item.User.Department;
                I.Position = item.User.Position;
                candidates.Add(I);
            }
            //candidates.UserId = semesterExam_Users.User.UserId;
            //candidates.UserName = semesterExam_Users.User.UserName;
            //candidates.FullName = semesterExam_Users.User.FullName;
            //candidates.Email = semesterExam_Users.User.Email;
            //candidates.Department = semesterExam_Users.User.Department;
            //candidates.Position = semesterExam_Users.User.Position;

            //ReportSemester reportSemester = new ReportSemester(semesterExam.SemesterName,semesterExam_Users.User.UserName,semesterExam.StartDay.ToString(),semesterExam.EndDay.ToString(),count);
            return candidates;

        }
    }
}
