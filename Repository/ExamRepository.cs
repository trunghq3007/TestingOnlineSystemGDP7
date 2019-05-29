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
		}

		public int Insert(Exam exam)
		{
            context.Exams.Add(new Exam
            {
                NameExam = exam.NameExam,
                CreateBy = exam.CreateBy,
                QuestionNumber = exam.QuestionNumber,
                Status = exam.Status,
                CreateAt = DateTime.Now,
                Note = exam.Note
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

                    context.Entry(exam).State = EntityState.Modified;
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

		public Exam Export_exam(int id)
		{
			var exams = context.Database.SqlQuery<ExportExam>(" select Row_number() over(order by q.id) stt, NameExam, QuestionNumber, e.Status, q.Content, q.Level, a.Content as 'ContentAnswer', a.IsTrue,c.Name from Exams e inner join ExamQuestions eq on e.Id = eq.ExamId inner join Question q on q.Id = eq.QuestionId inner join Answer a on a.Question_Id = q.Id inner join Category c on c.Id = q.Category_Id where e.Id = "+id).ToList();
            //Export<ExportExam> export = new Ẽz<ExportExam>();
            Export <ExportExam> export = new ExportExcel2007<ExportExam>();
			var data = export.ExportResult(exams);
			File.WriteAllBytes("D:/Export_Exam"+id+".xlsx", data);

			return null;
			
		}

		
	}
}
