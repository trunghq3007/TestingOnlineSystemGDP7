using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Model;
using Repository;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
	public class TestAssignmentService : ITestAssignmentService
	{
		private ITestAssignmentRepository repository;

		public TestAssignmentService()
		{
			repository = new TestAssignmentRepository(new DBEntityContext());
		}
		public List<User> GetById(int id)
		{
			return repository.GetById(id);
		}

		public List<User> GetAll(int id)
		{
			return repository.GetAll(id);
		}

		public int Insert(List<TestAssignment> items)
		{
			return repository.Insert(items);
		}
	}
}
