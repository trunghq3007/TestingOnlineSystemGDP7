using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;

namespace Services
{
	public class SemasterServices : Interfaces.IServices<SemesterExam>
	{
		private IRepository<SemesterExam> semaster;

		public SemasterServices()
		{
			semaster = new SemasterExamResponsitory(new DBEntityContext());
		}
		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SemesterExam> Filter(SemesterExam t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SemesterExam> GetAll()
		{
			return semaster.GetAll();
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
