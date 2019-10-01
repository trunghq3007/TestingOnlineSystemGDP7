using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository.Interfaces;

namespace Repository
{
    public class SemesterExamCustomer : Interfaces.ISemesterCustomer<SemesterExam, Test, Exam>, IDisposable
    {

        private DBEntityContext context;

        public SemesterExamCustomer(DBEntityContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SemesterExam> getAll()
        {
            return context.SemesterExams.Where(SE => SE.status == 1).ToList();
        }

        public IEnumerable<Test> getListExam(int id)
        {
            var list = context.Tests.Where(s => s.SemasterExamId == id).ToList();
            //var list = (from t in context.Tests
            //            join s in context.SemesterExams
            //            on t.SemasterExamId equals s.ID
            //            where t.SemasterExamId == id
            //            select new  {
            //                name=s.SemesterName
            //}
            //          ).ToList();
            return list;
        }
        public object getDetailExam(int id)
        {
            ExamInformation examInformation = new ExamInformation();
            var query = from T in context.Tests
                        join E in context.Exams on T.ExamId equals E.Id
                        join C in context.Categorys
                            on E.Category.Id equals C.Id
                        where T.Id == id
                        select new
                        {
                            T.TestName,
                            T.TestTime,
                            E.QuestionNumber,
                            C.Name,
                            T.Id,
							T.TotalTest
                        };
            examInformation.TestName = query.FirstOrDefault().TestName;
            examInformation.NumberChoiceQuestion = query.FirstOrDefault().QuestionNumber * 3 / 4;
            examInformation.NumberStatementQuestion = query.FirstOrDefault().QuestionNumber - examInformation.NumberChoiceQuestion;
            examInformation.TestTime = query.FirstOrDefault().TestTime;
            examInformation.CategoryName = query.FirstOrDefault().Name;
            examInformation.QuestionNumber = query.FirstOrDefault().QuestionNumber;
            examInformation.TotalScore = query.FirstOrDefault().TotalTest;
            return examInformation;
        }

        public IEnumerable<SemesterExam> SeachCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                return context.SemesterExams.Where(s => s.Code.Equals(code) && s.status !=0).ToList();
            }
           
            return context.SemesterExams.Where(s => s.status != 0).ToList();
        }
    }
}