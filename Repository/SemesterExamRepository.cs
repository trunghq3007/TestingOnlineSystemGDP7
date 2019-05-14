using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;

namespace Repository
{
  public  class SemesterExamRepository : Interfaces.ISemesterExamRepository<SemesterExam>, IDisposable
    { 
        private DBEntityContext context;

        public SemesterExamRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> Filter(SemesterExam t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> GetAll()
        {
            return context.SemesterExams.ToList();
        }

        public SemesterExam GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(SemesterExam t)
        {
            context.SemesterExams.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<SemesterExam> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public Model.ViewModel.ReportSemester Report(int id)
        {
            SemesterExam semesterExam = context.SemesterExams.Find(id);
            SemesterExam_User semesterExam_Users =
            context.SemesterExamUsers.Where(SU => SU.SemesterExam.ID == id && SU.Type == 1).First();
            var query = from E in context.Exams
                join T in context.Tests on E.Id equals T.Id
                join SE in context.SemesterExamUsers on T.ExamId equals SE.ID
                select E;
            
            int count = query.ToList().Count;
            ReportSemester reportSemester = new ReportSemester();
            reportSemester.SemesterName = semesterExam.SemesterName;
            User user= new User();
            //user.SemesterExam_Users.Where(S => S.ID == 1).ToList();
            //SemesterExam semesterExam= new SemesterExam();
            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            reportSemester.Creator = semesterExam_Users.User.FullName;







            //ReportSemester reportSemester = new ReportSemester(semesterExam.SemesterName,semesterExam_Users.User.UserName,semesterExam.StartDay.ToString(),semesterExam.EndDay.ToString(),count);
            return reportSemester;

        }

        public int Update(SemesterExam t)
        {
            throw new NotImplementedException();
        }
    }
}
