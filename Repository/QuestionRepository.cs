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

            if (model.CategoryId != null)
            {
                if (int.TryParse(model.Level, out int categoryId)) result = result.Where(s => s.Category.Id == categoryId).ToList();
            }
            if (model.TagsId != null)
            {
                if (int.TryParse(model.Level, out int tagId)) result = result.Where(s => s.Tags.Where(item => item.Id == tagId).Count() > 0).ToList();
            }
            if (model.CreatedBy.Count() > 0)
            {
                result = result.Where(s => model.CreatedBy.Equals(s.CreatedBy)).ToList();
            }

            if (model.Type != null)
            {
                if (int.TryParse(model.Level, out int type)) result = result.Where(s => s.Type == type).ToList();
            }

            if (model.Level != null)
            {
                if (int.TryParse(model.Level, out int level)) result = result.Where(s => s.Level == level).ToList();
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
            if (model.PageSize != null)
            {
                size = maxSize < 10 ? maxSize : 10;
            }
            else
            {
                if (int.TryParse(model.PageSize, out int pageSize))
                {
                    size = maxSize < pageSize ? maxSize : pageSize;
                }
            }
            var index = 1;
            if (model.PageIndex != null)
            {
                if (int.TryParse(model.PageIndex, out int pageIndex))
                    index = pageIndex <= 0 ? 1 : pageIndex;
            }


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
            t.CreatedBy = "anonymous user";
            t.CreatedDate = DateTime.Now;
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
            var trans = context.Database.BeginTransaction();
            var currenQuestion = context.Questions.Where(s => s.Id == t.Id).SingleOrDefault();
            var anserList = t.Answers.ToList();
            t.Category = context.Categorys.Where(s => s.Id == t.Category.Id).SingleOrDefault();

            currenQuestion.Answers = null;
            currenQuestion.Category = t.Category;
            currenQuestion.Content = t.Content;
            currenQuestion.CreatedBy = t.CreatedBy;
            currenQuestion.CreatedDate = t.CreatedDate;
            currenQuestion.Exams = t.Exams;
            currenQuestion.Level = t.Level;
            currenQuestion.Media = t.Media;
            currenQuestion.Tags = t.Tags;
            currenQuestion.Type = t.Type;
            currenQuestion.Suggestion = t.Suggestion;
            currenQuestion.UpdatedBy = "anonymous user";
            currenQuestion.UpdatedDate = DateTime.Now;
            context.Entry(currenQuestion).State = EntityState.Modified;
            context.Answers.RemoveRange(context.Answers.Where(s => s.Question.Id == t.Id));
            var result = context.SaveChanges();
            currenQuestion.Answers = t.Answers;
            context.SaveChanges();
            trans.Commit();
            return result;
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
                catch (Exception e)
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
