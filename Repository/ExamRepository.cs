using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Repository.Interfaces;

namespace Repository
{
    public class ExamRepository : Interfaces.IRepository<Exam>, IDisposable
    {
        private DBEntityContext context;

        public ExamRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> Filter(Exam t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> GetAll()
        {
            throw new NotImplementedException();
        }

        

        public int Insert(Exam t)
        {
            context.Exams.Add(t);
            return context.SaveChanges();
        }


        public IEnumerable<Exam> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Exam t)
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

        public Exam GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }
    }
}
