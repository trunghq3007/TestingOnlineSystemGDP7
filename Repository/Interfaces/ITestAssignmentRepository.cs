using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interfaces
{
	public interface ITestAssignmentRepository
	{
		List<User> GetById(int id);
		List<User> GetAll(int id);
		int Insert(List<TestAssignment> items);
	}
}
