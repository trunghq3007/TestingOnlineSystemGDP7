using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class SemasterExamResponsitory : Interfaces.IRepository<SemesterExam>, IDisposable
	{
		private DBEntityContext context;

		public SemasterExamResponsitory(DBEntityContext context)
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

		public IEnumerable<SemesterExam> Filter(SemesterExam t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SemesterExam> GetAll()
		{
			return context.SemesterExams;
		}

		public SemesterExam GetById(int id)
		{
			throw new NotImplementedException();
		}

		public int Insert(SemesterExam t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SemesterExam> Search(string searchString)
		{
			throw new NotImplementedException();
		}

		public int Update(SemesterExam t)
		{
			throw new NotImplementedException();
		}
	}
}
