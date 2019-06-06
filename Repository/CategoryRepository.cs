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
    public class CategoryRepository : Interfaces.ICategoryRepository<Category>, IDisposable
    {
        private DBEntityContext context;

        public CategoryRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            var item = context.Categorys.Where(s => s.Id == id).SingleOrDefault();
            try
            {
                if (item.Status == 0)
                {
                    context.Categorys.Remove(item);
                    return context.SaveChanges();
                }
            }
            catch
            {
                return 0;
            }

            return 0;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categorys.ToList();
        }

        public Category GetById(int id)
        {
            return context.Categorys.Where(s => s.Id == id).SingleOrDefault();
        }

        public int Insert(Category t)
        {
            context.Categorys.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<Category> Search(string searchString)
        {
            return context.Categorys.Where(s => s.Name.Contains(searchString));
        }

        public int Update(Category t)
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
            try
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    foreach (var id in arrId)
                    {
                        var flag = int.TryParse(id, out int ID);
                        if (flag)
                        {
                            context.Categorys.Remove(context.Categorys.Where(s => s.Id == ID).SingleOrDefault());
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
            catch
            {
                return 0;
            }
        }
    }

}
