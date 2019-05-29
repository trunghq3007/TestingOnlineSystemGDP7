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

        public IEnumerable<Question> Filter(QuestionFillterModel fillterModel)
        {
            var result = context.Questions.ToList();

            //if (fillterModel.CategoryId > 0)
            //{
            //    result = result.Where(s => s.Category.Id == fillterModel.CategoryId).ToList();
            //}
            //if (fillterModel.TagsId > 0)
            //{
            //    result = result.Where(s => s.Tags.Where(item => item.Id == fillterModel.TagsId).Count() > 0).ToList();
            //}
            if (fillterModel.CreatedBy != null && !"".Equals(fillterModel.CreatedBy))
            {
                result = result.Where(s => fillterModel.CreatedBy.Equals(s.CreatedBy)).ToList();
            }
            if (fillterModel.Type !=null && !"".Equals(fillterModel.Type))
            {
                result = result.Where(s => s.Type.ToString().Equals(fillterModel.Type)).ToList();
            }
            if (fillterModel.Level != null && !"".Equals(fillterModel.Level))
            {
                result = result.Where(s => s.Level.ToString().Equals(fillterModel.Level)).ToList();
             
            }
            //if (fillterModel.StartDate != null)
            //{
            //    result = result.Where(s => s.CreatedDate >= fillterModel.StartDate).ToList();
            //}
            //if (fillterModel.EndDate != null)
            //{
            //    result = result.Where(s => s.CreatedDate >= fillterModel.EndDate).ToList();
            //}

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
            if (!string.IsNullOrWhiteSpace(searchString)&& searchString !="undefined")
            {
                return context.Questions.Where(s => s.Content.Contains(searchString)).ToList();

            }

            return context.Questions.ToList();
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

        public GetFill listFilters()
        {
            GetFill item = new GetFill()
            {
                
                ListLevel=new HashSet<string>(),
                ListType = new HashSet<string>(),
                ListCreateBy = new HashSet<string>()
       
    };
            foreach (var it in context.Questions)
            {
                item.ListLevel.Add(it.Level.ToString());
                item.ListType.Add(it.Type.ToString());
                item.ListCreateBy.Add(it.CreatedBy);
            }
            
          

            return item;
        }
    }
}
