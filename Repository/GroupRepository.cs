using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
    public class GroupRepository : Interfaces.IRepository<Group>, IDisposable
    {
        private DBEntityContext context;

        public GroupRepository(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Filter(Group t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Group t)
        {
            context.Groups.Add(t);
            return context.SaveChanges();
        }


        public IEnumerable<Group> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Group t)
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

        public IEnumerable<Question> Filter(object t)
        {
            throw new NotImplementedException();
        }
    }
}
