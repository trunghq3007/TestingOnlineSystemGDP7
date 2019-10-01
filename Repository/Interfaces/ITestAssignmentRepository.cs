using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.ViewModel;

namespace Repository.Interfaces
{
	public interface ITestAssignmentRepository
	{
		List<User> GetById(int id);
		List<User> GetAll(int id);
		int Insert(List<TestAssignment> items);
		int Delete(List<TestAssignment> items);
		List<ViewTestResult> Result(TestResult item);
		int AddContent(List<Question> items,int userId,int testId);
		int UpdateScore(List<ViewTestResult> items, int userId, int testId);
	}
}
