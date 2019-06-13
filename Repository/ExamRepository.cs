using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Repository.Interfaces;
using Model.ViewModel;
using System.Data.Entity;
using System.IO;
using ExporterObjects;
using ExportImplementation;
using Microsoft.Office.Interop.Word;
using System.Drawing;


namespace Repository
{
	public class ExamRepository : Interfaces.IExamRepository<Exam>, IDisposable
	{
		private DBEntityContext context;
		public ExamRepository(DBEntityContext context)
		{
			this.context = context;
		}

		public int Delete(int id)
		{
			var item = context.Exams.Where(s => s.Id == id).SingleOrDefault();
			if (item != null)
			{
				if (item.Status != true)
				{
					context.Exams.Remove(item);
					return context.SaveChanges();
				}
				else
				{

				}


			}

			return 0;
		}



		public IEnumerable<Exam> Filter(Exam t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Exam> Filter(ExamFilterModel fillterModel)
		{
            var result = context.Exams.ToList();

            if (fillterModel.CreateBy != null && !"".Equals(fillterModel.CreateBy))
            {
                result = result.Where(s => s.CreateBy.Equals(fillterModel.CreateBy)).ToList();
            }
            if (fillterModel.TimeTest > 0)
            {
                result = result.Where(s => s.Tests != null && s.Tests.Where(t => t.TestTime == fillterModel.TimeTest).Count() > 0).ToList();
            }
            //if (fillterModel.CreateAt != null)
            //{
            //    result = result.Where(s => s.CreateAt == fillterModel.CreateAt).ToList();
            //}
            if (fillterModel.QuestionNumber > 0)
            {
                result = result.Where(s => s.QuestionNumber == fillterModel.QuestionNumber).ToList();

            }

            if (fillterModel.Status >= 0)
            {
                var flag = fillterModel.Status == 1 ? true : false;
                result = result.Where(s => s.Status == flag).ToList();

            }

            return result;

        }

		public IEnumerable<Exam> GetAll()
		{
            return context.Exams.ToList();
        }

        public Exam GetById(int id)
        {
            return context.Exams.Where(s => s.Id == id).SingleOrDefault();
           // return context.Users.Where(s => s.UserId == id).SingleOrDefault();
        }
        public IEnumerable<ViewDetailExam> GetDetailExams(int id)
        {
            var query = from e in context.Exams
                        join c in context.Categorys on e.Category.Id equals c.Id
                        where e.Id == id
                        select new ViewDetailExam()
                        {
                            Id = e.Id,
                            NameExam = e.NameExam,
                            CreateBy = e.CreateBy,
                            CreateAt = e.CreateAt,
                            QuestionNumber = e.QuestionNumber,
                            SpaceQuestionNumber = e.SpaceQuestionNumber,
                            Note = e.Note,
                            Status = e.Status,
                            NameCategory = c.Name,
                        };
            return query.ToList();

        }
        public int Insert(Exam exam)
		{

            //exam.Category = context.Categorys.Where(s => s.Category.Id == exam.Id).SingleOrDefault();
            context.Exams.Add(new Exam
            {
                NameExam = exam.NameExam,
                CreateBy = exam.CreateBy,
                QuestionNumber = exam.QuestionNumber,
                Status = exam.Status,
                SpaceQuestionNumber = exam.SpaceQuestionNumber,
                CreateAt = DateTime.Now,
                Note = exam.Note,
                Category = context.Categorys.SingleOrDefault(s => s.Id == exam.Category.Id)
        });
            return context.SaveChanges();
		}
		public IEnumerable<Exam> Search(string searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
			{
				return context.Exams.Where(s => s.NameExam.Contains(searchString)).ToList();
			}

			return context.Exams.ToList();
		}
		public int Update(Exam exam)
		{

            //exam.CreateAt=DateTime.Now;

            //context.Entry(exam).State = EntityState.Modified;
            //return context.SaveChanges();
            var currentExam = context.Exams.Find(exam.Id);

            currentExam.CreateAt = DateTime.Now;


            currentExam.Category = context.Categorys.SingleOrDefault(s => s.Id == exam.Category.Id);
           
            currentExam.NameExam = exam.NameExam;
            currentExam.CreateBy = exam.CreateBy;
            currentExam.QuestionNumber = exam.QuestionNumber;
            currentExam.Status = exam.Status;
            currentExam.SpaceQuestionNumber = exam.SpaceQuestionNumber;
           
            currentExam.Note = exam.Note;


            context.Entry(currentExam).State = EntityState.Modified;
            return context.SaveChanges();


        }
        public string GetCategoryName(int idExam)
        {
            string categoryName = "";
            var exam = context.Exams.SingleOrDefault(s => s.Id == idExam);
            if (exam != null)
            {
                categoryName = exam.Category.Name;
            }
            return categoryName;
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

		public ListFilter listFilters()
		{
			ListFilter item = new ListFilter
			{
				Listtmetest = new HashSet<float>(),
				ListCreateBy = new HashSet<string>(),
				Listquestion = new HashSet<int>()
			};
			foreach (var it in context.Tests)
			{
				item.Listtmetest.Add(it.TestTime);
			}
			foreach (var itw in context.Exams)
			{
				item.ListCreateBy.Add(itw.CreateBy);

			}
			foreach (var item1 in context.Exams)
			{
				item.Listquestion.Add(item1.QuestionNumber);
			}

			return item;

		}

        public string Export_exam(int id)
        {
            string Exam="";
            Exam exam = (from e in context.Exams
                         where e.Id == id
                         select e).SingleOrDefault();
            string examName = "";

            examName = "Subject : " + exam.NameExam.ToString() + "\r";
            

            try
            {
               

                string tempSave = string.Empty;
                int count = (from eq in context.ExamQuestions
                             where eq.ExamId == id
                             select new
                             {
                                 QuestionId = eq.QuestionId
                             }).Count();
                tempSave = "<h3 >" + examName + "</h3><p>" +"<h5> Question number: "+count+"<p></h5>";
                

                    var exams = (from eq in context.ExamQuestions
                                 where eq.ExamId == id
                                 select new
                                 {
                                     QuestionId = eq.QuestionId
                                 }).ToList();

                    int countExam = 1;
                    foreach (var item in exams)
                    {

                        long temp = Convert.ToInt64(item.QuestionId);
                        List<Question> ques = (from q in context.Questions
                                               where q.Id == temp

                                               select q).ToList();
                        foreach (Question itemq in ques)
                        {

                            string quesText = itemq.Content.ToString();

                            tempSave += "<br>Question " + countExam + " : " + itemq.Content + "<br>";
                            List<Answer> answers = (from a in context.Answers
                                                    where a.Question.Id == temp
                                                    select a
                                                    ).ToList();
                            int characterAbc = 0;
                            char character = 'A';
                            characterAbc = (int)character;
                            foreach (var itemAns in answers)
                            {
                                
                                string answer = (char)(characterAbc) + ". " + itemAns.Content.ToString()+"<br>";

                                tempSave += answer;
                                characterAbc++;
                            }
                            countExam++;
                        }

                    }
                    tempSave += "<br><br><br><br><br><br><br><br><br><br><br><br>" + "ANSWER" + "<br>";
                    //
                    int countExamAnswer = 1;
                    foreach (var item in exams)
                    {

                        long temp = Convert.ToInt64(item.QuestionId);
                        List<Question> ques = (from q in context.Questions
                                               where q.Id == temp

                                               select q).ToList();
                        foreach (Question itemq in ques)
                        {

                            string quesText = itemq.Content.ToString();

                            tempSave += "Q" + countExamAnswer + ": ";
                            List<Answer> answers = (from a in context.Answers
                                                    where a.Question.Id == temp
                                                    select a
                                                    ).ToList();
                            int characterAbc = 0;
                            char character = 'A';
                            characterAbc = (int)character;
                            foreach (var itemAns in answers)
                            {

                                string answer = (char)(characterAbc)+ "&emsp; &emsp;";
                                if (itemAns.IsTrue)
                                {
                                    tempSave += answer;
                                }

                                characterAbc++;
                            }
                            countExamAnswer++;
                        }

                    }
                    Exam = tempSave;
                    

            }
            catch (Exception ex)
            {
                throw;
            }

            return Exam;

        }
        public string SaveToTemporaryFile(string html)
        {
            string htmlTempFilePath = Path.Combine(Path.GetTempPath(), string.Format("{0}.html", Path.GetRandomFileName()));
            using (StreamWriter writer = File.CreateText(htmlTempFilePath))
            {
                html = string.Format("<html>{0}</html>", html);

                writer.WriteLine(html);
            }
            return htmlTempFilePath;
        }

    }
}
