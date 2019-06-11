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
            return context.SemesterExams.Where(SE => SE.status != 0).ToList();
        }

        public IEnumerable<Test> getListExam(int id)
        {
            var list = context.Tests.Where(s => s.SemasterExamId == id).ToList();
            return list;
        }
        public IEnumerable<Exam> getDetailExam(int id)
        {
            return context.Exams.Where(s => s.Id == id).ToList();
        }
    }
}