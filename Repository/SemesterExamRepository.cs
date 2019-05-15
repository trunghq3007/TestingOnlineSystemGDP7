using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;

namespace Repository
{
    public class SemesterExamRepository : Interfaces.ISemesterExamRepository<SemesterExam>, IDisposable
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
            return (IEnumerable<SemesterExam>)context.SemesterExams.ToList();
        }

        public SemesterExam GetById(int id)
        {
            return context.SemesterExams.Find(id);
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
                        join T in context.Tests on E.Id equals T.ExamId
                        join SE in context.SemesterExamUsers on T.ExamId equals SE.ID
                        select E;
            //var queryCandiates = from SEU in context.SemesterExamUsers
            //    join SE in context.SemesterExams on SEU.SemesterExam.ID equals id
            //    //where SEU.Type == 2
            //    select SEU;
            int numCandiates = context.SemesterExamUsers.Where(SEU => SEU.SemesterExam.ID == id && SEU.Type==2).ToList().Count;
            List<SemesterExam_User> list = context.SemesterExamUsers.Where(SEU => SEU.SemesterExam.ID == id && SEU.Type == 2).ToList();

            var QT = from TR in context.TestResults
                join U in context.Users on TR.UserId equals U.UserId
                join SU in context.SemesterExamUsers on U.UserId equals SU.User.UserId
                where SU.Type == 2 && SU.SemesterExam.ID == id
                select TR;
            int low = 0;
            int medium = 0;
            int good = 0;
            foreach (TestResult item in QT.ToList())
            {
                if (item.Score < 4)
                {
                    low++;
                }
                if (item.Score >7 )
                {
                    good++;
                }
                else
                {
                    medium++;
                }
            }

            float avgScore = 0;
            foreach (TestResult item  in QT.ToList())
            {
                avgScore += item.Score;
            }

            avgScore = avgScore / QT.ToList().Count;
            int participation = QT.ToList().Count;
            int count = query.ToList().Count;
            Exam exam = query.ToList().First();
            int numQuestion = exam.QuestionNumber;
          
            ReportSemester reportSemester = new ReportSemester();
            
            User user = new User();
            //user.SemesterExam_Users.Where(S => S.ID == 1).ToList();
            //SemesterExam semesterExam= new SemesterExam();
            List<User> users = context.Users.ToList();
            List<SemesterExam> semesterExamsterExams = context.SemesterExams.ToList();
            reportSemester.SemesterName = semesterExam.SemesterName;
            reportSemester.Creator = semesterExam_Users.User.FullName;
            reportSemester.StartDay = semesterExam.StartDay.ToString();
            reportSemester.EndDay = semesterExam.EndDay.ToString();
            reportSemester.NumEXams = count;
            reportSemester.NumQuestions = numQuestion;
            reportSemester.NumCandiates = numCandiates;
            reportSemester.NotParticipation = numCandiates - participation;
            reportSemester.AvgScore = avgScore;
            reportSemester.Good = good;
            reportSemester.Medium = medium;
            reportSemester.Low = low;
            //ReportSemester reportSemester = new ReportSemester(semesterExam.SemesterName,semesterExam_Users.User.UserName,semesterExam.StartDay.ToString(),semesterExam.EndDay.ToString(),count);
            return reportSemester;

        }

        public int Update(SemesterExam t)
        {
            throw new NotImplementedException();
        }
    }
}
