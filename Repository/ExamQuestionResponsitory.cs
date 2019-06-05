using DataAccessLayer;
using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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
            var result = (from e in context.Questions

                where   !(from q in context.ExamQuestions select q.QuestionId).ToList().Contains(e.Id)
                select new
                {
                    Id = e.Id,

                    Content = e.Content,
                    Level = e.Level,
                    Suggestion = e.Suggestion,
                    Type = e.Type,
                    Media = e.Media,
                    Status = e.Status,
                    CreateBy = e.CreatedBy,
                    CreateDate = e.CreatedDate
                }).ToList();
            List<Question> list = new List<Question>();
            foreach (var item in result)
            {
                Question Question = new Question();
                Question.Id = item.Id;

                Question.Content = item.Content;
                Question.Level = item.Level;
                Question.Suggestion = item.Suggestion;
                Question.Type = item.Type;
                Question.Status = item.Status;
                Question.CreatedBy = item.CreateBy;
                Question.CreatedDate = item.CreateDate;
                list.Add(Question);
            }

            return list;
        }

        public IEnumerable<Question> GetById(int id)
        {
            var examquestion = context.ExamQuestions.Where(e=>e.ExamId==id).ToList();
            var questions = context.Questions.ToList();
            List<Question> list=new List<Question>();
            foreach (var itemExamQuestion in examquestion)
            {
                questions.Remove(questions.SingleOrDefault(s => s.Id == itemExamQuestion.QuestionId));

            }

            foreach (var item in questions)
            {
                Question Question = new Question();
                Question.Id = item.Id;

                Question.Content = item.Content;
                Question.Level = item.Level;
                Question.Suggestion = item.Suggestion;
                Question.Type = item.Type;
                Question.Status = item.Status;
                Question.CreatedBy = item.CreatedBy;
                Question.CreatedDate = item.CreatedDate;
                list.Add(Question);
            }
            return list;
         

        }

        public IEnumerable<ViewQuestionExam> GetListQuestionById(int id)
        {

            var result = (from e in context.ExamQuestions
                join q in context.Questions on e.QuestionId equals q.Id
                join ex in context.Exams on e.ExamId equals ex.Id
                where e.ExamId == id
                select new
                {
                    Id = e.Id,
                    NameExam = ex.NameExam,
                    Content = q.Content,
                    Level = q.Level,
                    Suggestion = q.Suggestion,
                    Type = q.Type,
                    Media = q.Media,
                    Status = q.Status,
                    CreateBy = q.CreatedBy,
                    CreateDate = q.CreatedDate
                }).ToList();
            List<ViewQuestionExam> list = new List<ViewQuestionExam>();
            foreach (var item in result)
            {
                ViewQuestionExam ExamQuestion = new ViewQuestionExam();
                ExamQuestion.QuesId = item.Id;
                ExamQuestion.nameExam = item.NameExam;
                ExamQuestion.Content = item.Content;
                ExamQuestion.Level = item.Level;
                ExamQuestion.Suggestion = item.Suggestion;
                ExamQuestion.Type = item.Type;
                ExamQuestion.Status = item.Status;
                ExamQuestion.CreatedBy = item.CreateBy;
                ExamQuestion.CreatedDate = item.CreateDate;
                list.Add(ExamQuestion);
            }

            return list;
        }

        public int Insert(Model.ExamQuestion model)
        {
            var DetailQues = context.ExamQuestions
                .Where(q => q.QuestionId == model.QuestionId && q.ExamId == model.ExamId).ToList();
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

        public int AddMutipleQuestion(List<ExamQuestion> ListModel)
        {
            //var exam= context.Exams.Where(e=>e.Id==ListModel.Count())
            foreach (var item in ListModel)
            {
                context.ExamQuestions.Add(item);
            }

            return context.SaveChanges();
        }

       
        public int RandomQuestion(ViewQuestionExam model)
        {
            List<ExamQuestion> list=new List<ExamQuestion>();
            var questions = context.Questions.ToList();
            var count = context.Questions.ToList().Count();
           
            
            foreach (var item in questions)
            {
                ExamQuestion examquestion = new ExamQuestion();
                examquestion.QuestionId = item.Id;
                examquestion.ExamId =model.ExamId;
                list.Add(examquestion);
            }
            for (int i = 0; i < model.Total; i++)
            {
                var random = new Random();
                int randomnumber = random.Next(0, count);
                context.ExamQuestions.Add(list.ElementAt(randomnumber));


            }

            return context.SaveChanges();

        }
    }
}
