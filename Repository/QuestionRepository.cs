using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;

namespace Repository
{
    public class QuestionRepository : Interfaces.IRepository<Question>, IDisposable
    {
        private DBEntityContext context;

        public QuestionRepository(DBEntityContext context)
        {
            this.context = context;
        }

        public int Delete(int id)
        {
            var item = context.Questions.Where(s => s.Id == id).SingleOrDefault();
            if (item != null)
            {
                context.Questions.Remove(item);
                return context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Question> Filter(Question t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(object model)
        {
            var fillterModel = (QuestionFillterModel)model;
            var result = context.Questions.ToList();

            if (fillterModel.CategoryId > 0)
            {
                result = result.Where(s => s.Category.Id == fillterModel.CategoryId).ToList();
            }
            if (fillterModel.TagsId > 0)
            {
                result = result.Where(s => s.Tags.Where(item => item.Id == fillterModel.TagsId).Count() > 0).ToList();
            }
            if (fillterModel.CreatedBy.Count() > 0)
            {
                result = result.Where(s =>fillterModel.CreatedBy.Equals(s.CreatedBy)).ToList();
            }
            if (fillterModel.Type > 0)
            {
                result = result.Where(s => s.Type == fillterModel.Type).ToList();
            }
            if (fillterModel.Level > 0)
            {
                result = result.Where(s => s.Level == fillterModel.Level).ToList();
            }
            if (fillterModel.StartDate != null)
            {
                result = result.Where(s => s.CreatedDate >= fillterModel.StartDate).ToList();
            }
            if (fillterModel.EndDate != null)
            {
                result = result.Where(s => s.CreatedDate >= fillterModel.EndDate).ToList();
            }

            return result;
        }

        public IEnumerable<Question> GetAll()
        {
            return context.Questions.ToList();
        }

        public Question GetById(int id)
        {
            return context.Questions.Where(s => s.Id == id).SingleOrDefault();
        }

        public int Insert(Question t)
        {
            context.Questions.Add(t);
            return context.SaveChanges();
        }

        public IEnumerable<Question> Search(string searchString)
        {
            return context.Questions.Where(s => s.Content.Contains(searchString));
        }

        public int Update(Question t)
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
    }
}
