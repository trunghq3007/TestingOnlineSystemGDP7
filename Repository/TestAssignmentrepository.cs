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

		public int Delete(List<TestAssignment> items)
		{
			foreach (var item in items)
			{
				context.TestAssignments.Remove(context.TestAssignments.FirstOrDefault(x=>x.TestId==item.TestId&&x.UserId==item.UserId));
				
			}
			return context.SaveChanges();
		}

		public List<ViewTestResult> Result(TestResult item)
		{
			var test = context.Tests.Find(item.TestId);
			var queryQuestions = (from t in context.TestResults
				join q in context.Questions on t.QuestionId equals q.Id
				where t.TestId == item.TestId && t.UserId == item.UserId && t.TestTimeNo == item.TestTimeNo
				select new ViewTestResult
				{
					Answer = new AnswerModel
					{
						Content = t.Content,
					},
					Content = q.Content,
					TestName = test.TestName,
					TestTime = test.TestTime,
					Type = q.Type
				}).ToList();
			return queryQuestions;
		}

		public int AddContent(List<Question> items, int userId, int testId)
		{
			var checkCount = context.TestResults.OrderByDescending(x=>x.Id).FirstOrDefault(x => x.TestId == testId);
			var testResult=new TestResult
			{
				UserId = userId,
				Score = (checkCount != null) ? 1 : checkCount.TestTimeNo + 1,
				TestId = testId,
				AnwserId = 0
			};
			foreach (var item in items)
			{
				if (item.Answer != null)
				{
					testResult.Content = item.Answer;
					testResult.QuestionId = item.Id;
					context.TestResults.Add(testResult);
					context.SaveChanges();
				}
			}

			return context.SaveChanges();

		}

		public int UpdateScore(List<ViewTestResult> items, int userId, int testId)
		{
			foreach (var item in items)
			{
				
			}
		}
	}
}

