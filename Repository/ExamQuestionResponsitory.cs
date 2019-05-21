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
            throw new NotImplementedException();
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
            var ques=context.Database.SqlQuery<ViewQuestionExam>("select e.id, q.Content,q.Level,q.Suggestion,q.Type,q.Type,q.Media,q.Status,q.CreatedBy from Exams e inner join ExamQuestions eq on e.Id = eq.Exam_Id inner join Question q on q.Id = eq.Question_Id where e.id = @id",new SqlParameter("@id",id)).ToList();
            return ques;
        }

        public int Insert(Question t)
        {
            throw new NotImplementedException();
        }
        public int InsertExamQuestionExams(IEnumerable<ExamQuestion> obj)
        {
            var ques = context.Database.ExecuteSqlCommand("insert into ExamQuestions values (@idExam,@idQuestion)");

            foreach (var item in obj)
            {
                context.Database.Add(customer);

            }
            return true;
        }

        public IEnumerable<Question> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public int Update(Question t)
        {
            throw new NotImplementedException();
        }
    }
}
