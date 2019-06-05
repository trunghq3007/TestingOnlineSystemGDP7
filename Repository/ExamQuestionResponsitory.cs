using DataAccessLayer;
using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ExamQuestionResponsitory : Interfaces.IExamQuestion<Question>, IDisposable
    {
        public DBEntityContext context;

        public ExamQuestionResponsitory(DBEntityContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            var item = context.ExamQuestions.Where(s => s.QuestionId == id).SingleOrDefault();
            if (item != null)
            {
                context.ExamQuestions.Remove(item);
                return context.SaveChanges();
            }
            return 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Filter(Question t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ViewQuestionExam> GetListQuestionById(int id)
        {
            var ques=context.Database.SqlQuery<ViewQuestionExam>("  select q.id as 'QuesId',e.NameExam, q.Content,q.Level,q.Suggestion,q.Type,q.Media,q.Status,q.CreatedBy,q.CreatedDate from Exams e inner join ExamQuestions eq on e.Id = eq.ExamId inner join Question q on q.Id = eq.QuestionId where e.id = @id", new SqlParameter("@id",id)).ToList();
            return ques;
        }

        public int Insert(Model.ExamQuestion model)
        {
           var DetailQues = context.ExamQuestions.Where(q => q.QuestionId == model.QuestionId && q.ExamId==model.ExamId).ToList();
           if (DetailQues.Count <= 0)
           {
               context.ExamQuestions.Add(model);
               return context.SaveChanges();
            }

           return 0;
        }

        public IEnumerable<Question> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Question t)
        {
            throw new NotImplementedException();
        }
        public GetFill listFilters()
        {
            GetFill item = new GetFill()
            {
                ListLevel = new HashSet<string>(),
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
