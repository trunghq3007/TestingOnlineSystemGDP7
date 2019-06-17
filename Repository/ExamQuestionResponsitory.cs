using DataAccessLayer;
using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ExamQuestionResponsitory : Interfaces.IExamQuestion<Question>, IDisposable
    {
        public DBEntityContext context;

        public ExamQuestionResponsitory(DBEntityContext context)
        {
            this.context = context;
        }

       
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        

        public IEnumerable<ViewQuestionExam> GetAll()
        {
            var result = (from e in context.Questions
                          join c in context.Categorys on e.Category.Id equals c.Id
                          where !(from q in context.ExamQuestions select q.QuestionId).ToList().Contains(e.Id)
                          select new
                          {
                              Id = e.Id,
                              CategoryName = c.Name,
                              Content = e.Content,
                              Level = e.Level,
                              Suggestion = e.Suggestion,
                              Type = e.Type,
                              Media = e.Media,
                              Status = e.Status,
                              CreateBy = e.CreatedBy,
                              CreateDate = e.CreatedDate
                          }).ToList();
            List<ViewQuestionExam> list = new List<ViewQuestionExam>();
            foreach (var item in result)
            {
                ViewQuestionExam Question = new ViewQuestionExam();
                Question.QuesId = item.Id;
                Question.CategoryName = item.CategoryName;
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
        public IEnumerable<ViewQuestionExam> Filter(ViewQuestionExam fillterModel)
        {
            try
            {
                var result = (from e in context.Questions
                    join c in context.Categorys on e.Category.Id equals c.Id

                    select new
                    {
                        Id = e.Id,
                        CategoryName = c.Name,
                        Content = e.Content,
                        Level = e.Level,
                        Suggestion = e.Suggestion,
                        Type = e.Type,
                        Media = e.Media,
                        Status = e.Status,
                        CreateBy = e.CreatedBy,
                        CreateDate = e.CreatedDate
                    }).ToList();

                if (fillterModel.Level != null && !"".Equals(fillterModel.Level))
                {
                    result = result.Where(s => s.Level.Equals(fillterModel.Level)).ToList();
                }
                if (fillterModel.Type != null && !"".Equals(fillterModel.Type))
                {
                    result = result.Where(s => s.Type.Equals(fillterModel.Type)).ToList();
                }
                if (fillterModel.CreatedBy != null && !"".Equals(fillterModel.CreatedBy))
                {
                    result = result.Where(s => s.CreateBy.Equals(fillterModel.CreatedBy)).ToList();
                }

                if (fillterModel.CategoryName != null && !"".Equals(fillterModel.CategoryName))
                {
                    result = result.Where(s => s.CategoryName.Equals(fillterModel.CategoryName)).ToList();
                }
                List<ViewQuestionExam> list = new List<ViewQuestionExam>();
                foreach (var item in result)
                {
                    ViewQuestionExam Question = new ViewQuestionExam();
                    Question.QuesId = item.Id;
                    Question.CategoryName = item.CategoryName;
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
            catch (Exception e)
            {
                return GetAll();
            }
            

        }
        //getall
        public IEnumerable<ViewQuestionExam> GetById(int id)
        {
            
            var examquestion = context.ExamQuestions.Where(e => e.ExamId == id).ToList();
            //var questions = context.Questions.ToList();
            var questions = (from e in context.Questions
                             join c in context.Categorys on e.Category.Id equals c.Id

                             select new
                             {
                                 Id = e.Id,
                                 CategoryName = c.Name,
                                 Content = e.Content,
                                 Level = e.Level,
                                 Suggestion = e.Suggestion,
                                 Type = e.Type,
                                 Media = e.Media,
                                 Status = e.Status,
                                 CreateBy = e.CreatedBy,
                                 CreateDate = e.CreatedDate
                             }).ToList();
            List<ViewQuestionExam> list = new List<ViewQuestionExam>();
            foreach (var itemExamQuestion in examquestion)
            {
                questions.Remove(questions.SingleOrDefault(s => s.Id == itemExamQuestion.QuestionId));

            }

            foreach (var item in questions)
            {
                ViewQuestionExam Question = new ViewQuestionExam();
                Question.QuesId = item.Id;
                Question.CategoryName = item.CategoryName;
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

        public IEnumerable<ViewQuestionExam> GetListQuestionById(int id)
        {

            var result = (from e in context.ExamQuestions
                          join q in context.Questions on e.QuestionId equals q.Id
                          join ex in context.Exams on e.ExamId equals ex.Id
                          join c in context.Categorys on q.Category.Id equals c.Id
                          where e.ExamId == id
                          select new
                          {
                              Id = e.QuestionId,
                              NameExam = ex.NameExam,
                              Content = q.Content,
                              Level = q.Level,
                              Suggestion = q.Suggestion,
                              Type = q.Type,
                              Media = q.Media,
                              Status = q.Status,
                              CreateBy = q.CreatedBy,
                              CreateDate = q.CreatedDate,
                              CategoryName = c.Name,
                              Space = ex.SpaceQuestionNumber
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
                ExamQuestion.CategoryName = item.CategoryName;
                ExamQuestion.Space = item.Space;
                list.Add(ExamQuestion);
            }

            return list;
        }

       

        public IEnumerable<ViewQuestionExam> Search(string searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    var result = (from e in context.Questions
                        join c in context.Categorys on e.Category.Id equals c.Id
                        where e.Content.Contains(searchString)
                        select new
                        {
                            Id = e.Id,
                            CategoryName = c.Name,
                            Content = e.Content,
                            Level = e.Level,
                            Suggestion = e.Suggestion,
                            Type = e.Type,
                            Media = e.Media,
                            Status = e.Status,
                            CreateBy = e.CreatedBy,
                            CreateDate = e.CreatedDate
                        }).ToList();
                    List<ViewQuestionExam> list = new List<ViewQuestionExam>();
                    foreach (var item in result)
                    {
                        ViewQuestionExam Question = new ViewQuestionExam();
                        Question.QuesId = item.Id;
                        Question.CategoryName = item.CategoryName;
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
                var questions = (from e in context.Questions
                    join c in context.Categorys on e.Category.Id equals c.Id
                    
                    select new
                    {
                        Id = e.Id,
                        CategoryName = c.Name,
                        Content = e.Content,
                        Level = e.Level,
                        Suggestion = e.Suggestion,
                        Type = e.Type,
                        Media = e.Media,
                        Status = e.Status,
                        CreateBy = e.CreatedBy,
                        CreateDate = e.CreatedDate
                    }).ToList();
                List<ViewQuestionExam> listQuestion = new List<ViewQuestionExam>();
                foreach (var item in questions)
                {
                    ViewQuestionExam Question = new ViewQuestionExam();
                    Question.QuesId = item.Id;
                    Question.CategoryName = item.CategoryName;
                    Question.Content = item.Content;
                    Question.Level = item.Level;
                    Question.Suggestion = item.Suggestion;
                    Question.Type = item.Type;
                    Question.Status = item.Status;
                    Question.CreatedBy = item.CreateBy;
                    Question.CreatedDate = item.CreateDate;
                    listQuestion.Add(Question);
                }
                return listQuestion;

            }
            catch (Exception e)
            {
                return GetAll();
            }
            
        }

       
        public GetFill listFilters()
        {
            GetFill item = new GetFill()
            {
                ListLevel = new HashSet<string>(),
                ListType = new HashSet<string>(),
                ListCreateBy = new HashSet<string>(),
                ListCategory = new HashSet<string>()
            };
            foreach (var it in context.Questions)
            {
                item.ListLevel.Add(it.Level.ToString());
                item.ListType.Add(it.Type.ToString());
                item.ListCreateBy.Add(it.CreatedBy);
              
            }

            foreach (var category in context.Questions)
            {
                try
                {
                    var cateogories = context.Categorys.Where(c => c.Id == category.Category.Id).SingleOrDefault();

                    if (category.Category.Id == cateogories.Id)
                    {
                        item.ListCategory.Add(category.Category.Name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }




            }

            return item;
        }

        public int AddMutipleQuestion(List<ExamQuestion> ListModel)
        {
            try
            {
                var check = ListModel.ElementAt(0);
                var exam = context.Exams.Where(ex => ex.Id == check.ExamId).SingleOrDefault();

                var countExamQuestion = context.ExamQuestions.Where(ex => ex.ExamId == exam.Id).ToList().Count();
               
                List<ExamQuestion> list = new List<ExamQuestion>();
                List<ExamQuestion> listExa = new List<ExamQuestion>();
                foreach (var item in ListModel)
                {

                    var Ques = context.Questions.Where(e => e.Category.Id == exam.Category.Id && e.Id == item.QuestionId && exam.Id == item.ExamId).SingleOrDefault();
                    if (Ques != null)
                    {
                        ExamQuestion ExamQues = new ExamQuestion();
                        ExamQues.QuestionId = Ques.Id;
                        ExamQues.ExamId = check.ExamId;
                        list.Add(ExamQues);
                    }

                }

                if (list.Count < exam.SpaceQuestionNumber - countExamQuestion)
                {

                    for (int i = 0; i < list.Count; i++)
                    {
                        var checkModel = list.ElementAt(i).QuestionId;
                        var checkQuestion = context.Questions.Where(ex => ex.Id == checkModel && ex.Category.Id == exam.Category.Id)
                            .SingleOrDefault();
                        if (checkQuestion != null)
                        {
                            ExamQuestion examQuestion = new ExamQuestion();
                            examQuestion.ExamId = exam.Id;
                            examQuestion.QuestionId = checkQuestion.Id;

                            context.ExamQuestions.Add(examQuestion);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < exam.SpaceQuestionNumber - countExamQuestion; i++)
                    {
                        var checkModel = list.ElementAt(i).QuestionId;
                        var checkQuestion = context.Questions.Where(ex => ex.Id == checkModel && ex.Category.Id == exam.Category.Id)
                            .SingleOrDefault();
                        if (checkQuestion != null)
                        {
                            ExamQuestion examQuestion = new ExamQuestion();
                            examQuestion.ExamId = exam.Id;
                            examQuestion.QuestionId = checkQuestion.Id;

                            context.ExamQuestions.Add(examQuestion);
                    }
                }
                }

                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return -2;
            }
            
        }


        public int RandomQuestion(ViewQuestionExam model)
        {
            try
            {
                List<ExamQuestion> list = new List<ExamQuestion>();
                var exam = context.Exams.Where(ex => ex.Id == model.ExamId).SingleOrDefault();
                var questions = context.Questions.Where(e => e.Category.Name == model.CategoryName && e.Level == model.Type).ToList();
                var examquestion = context.ExamQuestions.Where(ex => ex.ExamId == model.ExamId).ToList();
                foreach (var item in examquestion)
                {
                    questions.Remove(questions.SingleOrDefault(s => s.Id == item.QuestionId));
                }

                ExamQuestion ExamQues;
                foreach (var item in questions)
                {
                    ExamQues = new ExamQuestion();


                    ExamQues.QuestionId = item.Id;
                    ExamQues.ExamId = model.ExamId;
                    list.Add(ExamQues);
                }

                if (model.Total >= list.Count) model.Total = list.Count;
                Random random;
                int count = questions.Count;

                int checkSpace = exam.SpaceQuestionNumber - examquestion.Count;
                if (checkSpace < model.Total)
                {



                    for (int i = 0; i < checkSpace; i++)
                    {

                        random = new Random();
                        count--;
                        int randomnumber = random.Next(0, count);



                        context.ExamQuestions.Add(list.ElementAt(randomnumber));
                        list.Remove(list.ElementAt(randomnumber));



                    }
                }
                else
                {
                    for (int i = 0; i < model.Total; i++)
                    {

                        random = new Random();
                        count--;
                        int randomnumber = random.Next(0, count);



                        context.ExamQuestions.Add(list.ElementAt(randomnumber));
                        list.Remove(list.ElementAt(randomnumber));



                    }

                }


                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return -2;
            }

           

        }

        public int DeleteMutiple(List<ExamQuestion> ListModel)
        {
            try
            {
                var index = ListModel.ElementAt(0).ExamId;
                var exam = context.Exams.Where(e => e.Id == index).SingleOrDefault();
                if (exam.Status == false)
                {
                    foreach (var item in ListModel)
                    {


                        try
                        {
                            var List = context.ExamQuestions
                                .Where(eq => eq.ExamId == item.ExamId && eq.QuestionId == item.QuestionId).SingleOrDefault();
                            if (List != null)
                            {
                                context.ExamQuestions.Remove(List);
                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }



                    }
                    return context.SaveChanges();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return -2;
            }
           
          
        }


    }
}
