using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
    public class TagRepository : Interfaces.IRepository<Tag>, IDisposable
    {
        private DBEntityContext context;

        public TagRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> Filter(Tag t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            return context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Tag t)
        {
            context.Tags.Add(t);
            return context.SaveChanges();
        }


        public IEnumerable<Tag> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Tag t)
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
    }
}
