using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Model.ViewModel;
using Repository.Interfaces;

namespace Repository
{
	public class TestAssignmentRepository: ITestAssignmentRepository
	{
		private DBEntityContext context;

		public TestAssignmentRepository(DBEntityContext context)
		{
			this.context = context;
		}
		public List<User> GetById(int id)
		{
			throw new Exception();

		}

		public List<User> GetAll(int id)
		{
			throw new Exception();

		}

		public int Insert(List<TestAssignment> items)
		{
			context.TestAssignments.AddRange(items);
			return context.SaveChanges();
		}

		public int Delete(List<TestAssignment> items)
		{
			throw new Exception();

		}

		public List<ViewTestResult> Result(TestResult item)
		{
			throw new Exception();

		}

		public int AddContent(List<Question> items, int userId, int testId)
		{
			throw new Exception();

		}

		public int UpdateScore(List<ViewTestResult> items, int userId, int testId)
		{
			foreach (var item in items)
			{
				
			}
		}
	}
}

