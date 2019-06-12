using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository.Interfaces;

namespace Repository
{
    public class TagRepository : Interfaces.ITagRepository<Tag>, IDisposable
    {
        private DBEntityContext context;

        public TagRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            var item = context.Tags.Where(s => s.Id == id).SingleOrDefault();
            if (item != null)
            {
                context.Tags.Remove(item);
                return context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Tag> GetAll()
        {
            return context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return context.Tags.Where(s => s.Id == id).SingleOrDefault();
        }

        public int Insert(Tag t)
        {
            context.Tags.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<Tag> Search(string searchString)
        {
            return context.Tags.Where(s => s.Name.Contains(searchString));
        }

        public int Update(Tag t)
        {
            context.Entry(t).State = EntityState.Modified;
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

        public int DeleteBatch(string listId)
        {
            var arrId = listId.Split(',');
            using (var trans = context.Database.BeginTransaction())
            {
                foreach (var id in arrId)
                {
                    var flag = int.TryParse(id, out int ID);
                    if (flag)
                    {
                        context.Tags.Remove(context.Tags.Where(s => s.Id == ID).SingleOrDefault());
                    }
                    else
                    { 

                        trans.Rollback();
                        return 0;
                    }
                }
                var result = context.SaveChanges();
                trans.Commit();
                return result;
            }
        }


    }
}