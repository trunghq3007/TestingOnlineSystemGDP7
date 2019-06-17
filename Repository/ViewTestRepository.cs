using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
public	class ViewTestRepository : ITestRepository<ViewTest>, IDisposable
	{
		private DBEntityContext context;

		public ViewTestRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ViewTest> Filter(ViewTest t)
		{
			throw new NotImplementedException();
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
            foreach(var item in result)
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
			// return context..ToList();
			//var ques = context.Database.SqlQuery<ViewTest>
			//	("select t.Id,t.TestName,t.CreateBy,PassScore, e.NameExam,s.SemesterName,t.Status from Tests t join Exams e on t.ExamId = e.Id join SemesterExam s on t.SemasterExamId = s.ID").ToList();
			//return ques.ToList();
		}



		public int Insert(ViewTest test)
		{
			//context.Tests.Add(test);
			return context.SaveChanges();
		}


		public IEnumerable<ViewTest> Search(string searchString)
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

		public int Update(ViewTest t)
		{
			throw new NotImplementedException();
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


		//public Test GetById(int id)
		//{
		//    var ques = context.Database.SqlQuery<Test>("select t.Id,t.TestName, t.ExamId,t.CreateBy,PassScore, t.SemesterExam_ID from Tests t join Exams e on t.ExamId = e.Id join SemesterExam s on t.SemesterExam_ID = s.ID").ToList();
		//    return ques;
		//}
		//public IEnumerable<Test> GetList()
		//{
		//    var ques = context.Database.SqlQuery<Test>
		//        ("select t.Id,t.TestName,t.CreateBy,PassScore, e.NameExam,s.SemesterName,t.Status from Tests t join Exams e on t.ExamId = e.Id join SemesterExam s on t.SemasterExamId = s.ID").ToList();
		//    return ques;

		//}

		public IEnumerable<Test> Filter(Test t)
		{
			throw new NotImplementedException();
		}

		//public Test GetById(int id)
		//{
		//    throw new NotImplementedException();
		//}

		public Test GetById(int id)
		{
			return context.Tests.Where(s => s.Id == id).SingleOrDefault();
		}



		public ViewTest Export_exam(int id)
		{
			throw new NotImplementedException();
		}

		List<ViewDetailTest> ITestRepository<ViewTest>.GetById(int id)
		{
			throw new NotImplementedException();
		}

        public ViewTest GetByTestId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
