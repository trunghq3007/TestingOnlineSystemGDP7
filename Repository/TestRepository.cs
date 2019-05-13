using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
namespace Repository
{
    public class TestRepository : Interfaces.IRepository<Test>, IDisposable
    {
        private DBEntityContext context;

        public TestRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> Filter(Test t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Test> GetAll()
        {
            throw new NotImplementedException();
        }



        public int Insert(Test t)
        {
            context.Tests.Add(t);
            return context.SaveChanges();
        }


        public IEnumerable<Test> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Test t)
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

        public Test GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }
    }
}
