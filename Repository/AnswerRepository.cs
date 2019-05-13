using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
    public class AnswerRepository : Interfaces.IRepository<Answer>, IDisposable
    {
        private DBEntityContext context;

        public AnswerRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            var item = context.Answers.Where(s => s.Id == id).SingleOrDefault();
            if (item != null)
            {
                context.Answers.Remove(item);
                return context.SaveChanges();
            }
            else
            {
                //log
            }
            return 0;

        }

        public IEnumerable<Answer> Filter(Answer t)
        {
            return null;
        }

        public IEnumerable<Answer> GetAll()
        {
            return context.Answers.ToList();
        }

        public Answer GetById(int id)
        {
            return context.Answers.Where(s => s.Id == id).SingleOrDefault();
        }

        public int Insert(Answer t)
        {
            context.Answers.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<Answer> Search(string searchString)
        {
            return context.Answers.Where(s => s.Content == searchString).ToList ();
        }

        public int Update(Answer t)
        {
            context.Entry(t).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
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

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }
    }
}
