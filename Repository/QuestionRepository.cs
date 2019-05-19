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
    public class QuestionRepository : Interfaces.IQuestionRepository<Question>, IDisposable
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

        public IEnumerable<Question> Filter(QuestionFillterModel model)
        {
            var result = context.Questions.ToList();

            if (model.CategoryId > 0)
            {
                result = result.Where(s => s.Category.Id == model.CategoryId).ToList();
            }
            if (model.TagsId > 0)
            {
                result = result.Where(s => s.Tags.Where(item => item.Id == model.TagsId).Count() > 0).ToList();
            }
            if (model.CreatedBy.Count() > 0)
            {
                result = result.Where(s =>model.CreatedBy.Equals(s.CreatedBy)).ToList();
            }
            if (model.Type > 0)
            {
                result = result.Where(s => s.Type == model.Type).ToList();
            }
            if (model.Level > 0)
            {
                result = result.Where(s => s.Level == model.Level).ToList();
            }
            if (model.StartDate != null)
            {
                result = result.Where(s => s.CreatedDate >= model.StartDate).ToList();
            }
            if (model.EndDate != null)
            {
                result = result.Where(s => s.CreatedDate >= model.EndDate).ToList();
            }

            var size = 10;
            var maxSize = result.Count();
            if (model.PageSize <= 0)
            {
                size = maxSize < 10 ? maxSize : 10;
            }
            else
            {
                size = maxSize < model.PageSize ? maxSize : model.PageSize;
            }
            var index = model.PageIndex <= 0 ? model.PageIndex = 1 : model.PageIndex;

            var start = (index - 1) * size;
            var end = index * size - 1;
            return result.GetRange(start, end);
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

        public IEnumerable<Question> Search(SearchPaging item)
        {
            var result = context.Questions.Where(s => s.Content.Contains(item.SearchString)).ToList();
            var size = 10;
            var maxSize = result.Count();
            if (item.PageSize <= 0)
            {
                size = maxSize < 10 ? maxSize : 10;
            }
            else
            {
                size = maxSize < item.PageSize ? maxSize : item.PageSize;
            }
            var index = item.PageIndex <= 0 ? item.PageIndex = 1 : item.PageIndex;

            var start = (index - 1) * size;
            var end = index * size - 1;
            return result.GetRange(start, end);
        }

        public int Update(Question t)
        {
            context.Entry(t).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public Category getCategoryByName(string cateName)
        {
            return context.Categorys.Where(s => s.Name.Equals(cateName)).FirstOrDefault();
        }

        public string Import(List<Question> list)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var question in list)
                    {
                        context.Questions.Add(question);
                    }
                    context.SaveChanges();
                    transaction.Commit();
                    return "OK";
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    return e.Message;
                }
              
            }
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
