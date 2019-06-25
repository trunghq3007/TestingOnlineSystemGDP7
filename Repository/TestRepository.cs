using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Model.ViewModel;
using Repository.Interfaces;
using System.Data.Entity;

namespace Repository
{
    public class TestRepository : Interfaces.ITestRepository<Test>, IDisposable
	{
		private DBEntityContext context;

		public TestRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public int Delete(int id)
		{
            var item = context.Tests.Where(s => s.Id == id).SingleOrDefault();
            if (item != null)
            {
                if (item.Status == 0)
                {
                    context.Tests.Remove(item);
                    return context.SaveChanges();
                }
                else
                {

                }


            }

            return 0;
        }

		public IEnumerable<Test> Filter(Test t)
		{
			throw new NotImplementedException();
		}

       

        public int Insert(Test t)
		{
			context.Tests.Add(new Test()
            {
                ExamId=t.ExamId,
                SemasterExamId = t.SemasterExamId,
                Status = t.Status,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CreateBy = t.CreateBy,
                PassScore = t.PassScore,
                TestName = t.TestName,
                TotalTest = t.TotalTest,
                TestTime = t.TestTime
            });
			return context.SaveChanges();
		}


		//public IEnumerable<Test> Search(string searchString)
		//{
		//	if (!string.IsNullOrEmpty(searchString))
		//	{
		//		return context.Tests.Where(s => s.TestName.Contains(searchString));
		//	}

		//	return context.Tests.ToList();
		//}

		public int Update(Test test)
        {
            // var item = context.Tests.Where(s => s.Id == test.Id).SingleOrDefault();
            var currentTest = context.Tests.Where(s => s.Id == test.Id).SingleOrDefault();
            var semasterList = test.SemesterExam;
            
            test.Exam = context.Exams.Where(s => s.Id == test.ExamId).SingleOrDefault();
            currentTest.SemesterExam = null;
            currentTest.Exam = test.Exam;
            currentTest.SemasterExamId = test.SemasterExamId;
            currentTest.ExamId = test.ExamId;
            currentTest.Status = test.Status;
            currentTest.CreateBy = test.CreateBy;
            currentTest.PassScore = test.PassScore;
            currentTest.TestName = test.TestName;
            currentTest.TotalTest = test.TotalTest;
            currentTest.TestTime = test.TestTime;
            
            context.Entry(currentTest).State = EntityState.Modified;
            return context.SaveChanges();
            //throw new NotImplementedException();
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

		

		public IEnumerable<Question> Filter(object t)
		{
			throw new NotImplementedException();
		}

		public Test Export_exam(int id)
		{
			throw new NotImplementedException();
		}

		

		List<ViewDetailTest> ITestRepository<Test>.GetById(int id)
		{
            var query = from t in context.Tests
                        join ts in context.TestResults on t.Id equals ts.TestId
                        join u in context.Users on ts.UserId equals u.UserId
                        join e in context.Exams on t.ExamId equals e.Id
                        join c in context.Categorys on e.Category.Id equals c.Id
                        join s in context.SemesterExams on t.SemasterExamId equals s.ID
                        where t.Id == id
                        select new ViewDetailTest()
                        {
                            Id = t.Id,
                            NameExam = e.NameExam,
                            NameCategory = c.Name,
                            TestName = t.TestName,
                            NameUser = u.UserName,
                            SemsesterName = s.SemesterName
                        };
            return query.ToList();
        }

        public IEnumerable<ViewTest> GetAll()
        {
            var result = from t in context.Tests
                         join e in context.Exams on t.ExamId equals e.Id
                         join s in context.SemesterExams on t.SemasterExamId equals s.ID
                         // where t.Status != 0
                         select new

                         {


                             t.Id,
                             t.TestName,
                             t.CreateBy,
                             t.PassScore,
                             e.NameExam,
                             s.SemesterName,
                             t.Status
                         };
            List<ViewTest> list = new List<ViewTest>();
            foreach (var item in result)
            {
                ViewTest viewTest = new ViewTest();
                viewTest.Id = item.Id;
                viewTest.TestName = item.TestName;
                viewTest.CreateBy = item.CreateBy;
                viewTest.PassScore = item.PassScore;
                viewTest.NameExam = item.NameExam;
                viewTest.SemesterName = item.SemesterName;
                viewTest.Status = item.Status;
                list.Add(viewTest);
            }
            return list;
            //var ques = context.Database.SqlQuery<ViewTest>
            //    ("select t.Id,t.TestName,t.CreateBy,PassScore, e.NameExam,s.SemesterName,t.Status from Tests t join Exams e on t.ExamId = e.Id join SemesterExam s on t.SemasterExamId = s.ID").ToList();
            //return ques.ToList();
        }

        public Test GetByTestId(int id)
        {
            return context.Tests.Where(s => s.Id == id).SingleOrDefault();
        }

        //IEnumerable<ViewTest> ITestRepository<Test>.Search(string searchString)
        //{
        //    throw new NotImplementedException();
        //}
        public IEnumerable<ViewTest> Search(string searchString)
        {
            if (searchString != null&& searchString!="undefined")
            {
                //var ques = context.Database.SqlQuery<ViewTest>
                //("  select t.Id,t.TestName,t.CreateBy,PassScore, e.NameExam,s.SemesterName,t.Status from Tests t join Exams e on t.ExamId = e.Id join SemesterExam s on t.SemasterExamId = s.ID where t.TestName like '%"+searchString+"%'").ToList();
                var result = from t in context.Tests
                             join e in context.Exams on t.ExamId equals e.Id
                             join s in context.SemesterExams on t.SemasterExamId equals s.ID
                             where t.TestName.Contains(searchString)
                             select new
                             {
                                 t.Id,
                                 t.TestName,
                                 t.CreateBy,
                                 t.PassScore,
                                 e.NameExam,
                                 s.SemesterName
                             };
                List<ViewTest> list = new List<ViewTest>();
                foreach (var item in result)
                {
                    ViewTest viewTest = new ViewTest();
                    viewTest.Id = item.Id;
                    viewTest.TestName = item.TestName;
                    viewTest.CreateBy = item.CreateBy;
                    viewTest.PassScore = item.PassScore;
                    viewTest.NameExam = item.NameExam;
                    viewTest.SemesterName = item.SemesterName;
                    list.Add(viewTest);
                }
                return list;
            }
           
                var resultt = from t in context.Tests
                             join e in context.Exams on t.ExamId equals e.Id
                             join s in context.SemesterExams on t.SemasterExamId equals s.ID
                            
                             select new
                             {
                                 t.Id,
                                 t.TestName,
                                 t.CreateBy,
                                 t.PassScore,
                                 e.NameExam,
                                 s.SemesterName
                             };
                List<ViewTest> listt = new List<ViewTest>();
                foreach (var item in resultt)
                {
                    ViewTest viewTest = new ViewTest();
                    viewTest.Id = item.Id;
                    viewTest.TestName = item.TestName;
                    viewTest.CreateBy = item.CreateBy;
                    viewTest.PassScore = item.PassScore;
                    viewTest.NameExam = item.NameExam;
                    viewTest.SemesterName = item.SemesterName;
                    listt.Add(viewTest);
                }
            
            
            return listt;
        }


        
    }
}
