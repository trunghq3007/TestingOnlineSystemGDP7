using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
  public  class SemesterExamRepository : Interfaces.IRepository<SemesterExam>, IDisposable
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

        public int Update(SemesterExam t)
        {
            throw new NotImplementedException();
        }
    }
}
