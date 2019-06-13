using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;

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
            try
            {
                var item = context.Questions.Where(s => s.Id == id).SingleOrDefault();
                if (item != null)
                {
                    //item.Answers = null;
                    //context.SaveChanges();
                    //context.Questions.Remove(item);
                    item.Status = -1;
                    return context.SaveChanges();
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<Question> Filter(Question t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(QuestionFillterModel model)
        {
            try
            {
                var result = context.Questions.ToList();

                if (model.CategoryId != null && !"".Equals(model.CategoryId))
                {
                    if (int.TryParse(model.CategoryId, out int categoryId)) result = result.Where(s => s.Category.Id == categoryId).ToList();
                }
                if (model.TagsId != null && !"".Equals(model.TagsId))
                {
                    if (int.TryParse(model.TagsId, out int tagId)) result = result.Where(s => s.Tags.Where(item => item.Id == tagId).Count() > 0).ToList();
                }
                if (model.CreatedBy != null && !"".Equals(model.CreatedBy))
                {
                    result = result.Where(s => model.CreatedBy.Equals(s.CreatedBy)).ToList();
                }

                if (model.Type != null && !"".Equals(model.Type))
                {
                    if (int.TryParse(model.Type, out int type)) result = result.Where(s => s.Type == type).ToList();
                }

                if (model.Level != null && !"".Equals(model.Level))
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
                //return result.GetRange(start, end);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<Question> GetAll()
        {
            try
            {
                return context.Questions.Where(s => s.Status != -1).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }

        }

        public Question GetById(int id)
        {
            try
            {
                return context.Questions.Where(s => s.Id == id).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int Insert(Question t)
        {
            try
            {
                t.CreatedBy = "anonymous user";
                t.CreatedDate = DateTime.Now;
                if (t.Category != null)
                {
                    t.Category = context.Categorys.SingleOrDefault(s => s.Id == t.Category.Id);
                }
                if (t.Tags != null)
                {
                    var tags = new List<Tag>();
                    foreach (var tag in t.Tags)
                    {
                        tags.Add(context.Tags.SingleOrDefault(s => s.Id == tag.Id));
                    }
                    t.Tags = tags;
                }
                context.Questions.Add(t);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public IEnumerable<Question> Search(SearchPaging item)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }

        }

        public int Update(Question t)
        {
            var trans = context.Database.BeginTransaction();
            try
            {

                var currenQuestion = context.Questions.Where(s => s.Id == t.Id).SingleOrDefault();
                var anserList = t.Answers.ToList();
                t.Category = context.Categorys.Where(s => s.Id == t.Category.Id).SingleOrDefault();

                currenQuestion.Answers = null;
                currenQuestion.Category = t.Category;
                currenQuestion.Content = t.Content;
                currenQuestion.ExamQuestions = t.ExamQuestions;
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
            catch (Exception e)
            {
                trans.Rollback();
                throw e;
            }

        }

        public Category getCategoryByName(string cateName)
        {
            return context.Categorys.FirstOrDefault(s => s.Name.Equals(cateName));
        }

        public int Import(List<Question> list)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var result = 1;
                    foreach (var question in list)
                    {
                        var cate = context.Categorys.Where(s => s.Name.ToLower().Equals(question.Category.Name.ToLower())).ToList();
                        question.Category = cate.Count() <= 0 ? question.Category : cate.First();
                        context.Questions.Add(question);
                        result = context.SaveChanges();
                        if (result <= 0)
                        {
                            transaction.Rollback();
                            return result;
                        };
                    }

                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
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
