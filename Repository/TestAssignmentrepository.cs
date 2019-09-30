using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
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
			var list = (from testAssignment in context.TestAssignments.ToList()
				join test in context.Tests.ToList() on testAssignment.TestId equals test.Id
				join user in context.Users.ToList() on testAssignment.UserId equals user.UserId
				where test.Id==id
				select user).ToList();
			return list;
		}

		public List<User> GetAll(int id)
		{
			var us = context.Users.ToList();
			var list = (from user in context.Users.ToList()
				where !(from t in context.TestAssignments select t.UserId).ToList().Contains(user.UserId)
						select user).ToList();
			return list;
		}

		public int Insert(List<TestAssignment> items)
		{
			context.TestAssignments.AddRange(items);
			return context.SaveChanges();
		}
	}
}

