using Model;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class TestAssignmentController : ApiController
    {
	    private TestAssignmentService testAssignmentService;
	    public TestAssignmentController()
	    {
		    testAssignmentService = new TestAssignmentService();
	    }
	    public string GetResult(int UserId,int TestId,int TestTimeNo)
	    {
			    ResultObject resultt = new ResultObject();
			    try
			    {
					var testResult=new TestResult
					{
						UserId = UserId,
						TestId = TestId,
						TestTimeNo = TestTimeNo
					};
				var result = testAssignmentService.Result(testResult);
				    return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
				    {
					    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				    });
			    }
			    catch (Exception e)
			    {
				    resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
				    return JsonConvert.SerializeObject(resultt);
			    }

		    
		}

		public string Get([FromUri] string action, [FromUri] int id)
		{
			if ("GetAll".Contains(action))
			{
				ResultObject resultt = new ResultObject();

				try
				{
					var result = testAssignmentService.GetAll(id).ToList();
					return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
					{
						ReferenceLoopHandling = ReferenceLoopHandling.Ignore
					});
				}
				catch (Exception e)
				{

					resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
					return JsonConvert.SerializeObject(resultt);
				}

			}

			if ("GetById".Contains(action))
			{
				ResultObject resultt = new ResultObject();
				try
				{
					var result = testAssignmentService.GetById(id).ToList();
					return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
					{
						ReferenceLoopHandling = ReferenceLoopHandling.Ignore
					});
				}
				catch (Exception e)
				{
					resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
					return JsonConvert.SerializeObject(resultt);
				}

			}
			
			return "true";
		}

		[System.Web.Http.HttpPost]
		public string Insert([FromUri]string action, [FromBody]object value)
		{
			if ("AddMutiple".Equals(action))
			{
				ResultObject resultt = new ResultObject();
				try
				{
					var testAssignments = JsonConvert.DeserializeObject<List<TestAssignment>>(value.ToString());
					var add = testAssignmentService.Insert(testAssignments);
					return JsonConvert.SerializeObject(add);
				}
				catch (Exception e)
				{
					resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
					return JsonConvert.SerializeObject(resultt);
				}

			}

			if ("DeleteMutiple".Equals(action))
			{
				ResultObject resultt = new ResultObject();

				try
				{
					var testAssignments = JsonConvert.DeserializeObject<List<TestAssignment>>(value.ToString());
					var delete = testAssignmentService.Delete(testAssignments);
					return JsonConvert.SerializeObject(delete);
				}
				catch (Exception e)
				{
					resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
					return JsonConvert.SerializeObject(resultt);
				}

			}
			

			return "true";
		}

		[System.Web.Http.HttpPost]
		public string Add([FromBody] object value, int testId, int userId)
		{
			ResultObject resultt = new ResultObject();

			try
			{
				var testAssignments = JsonConvert.DeserializeObject<List<Question>>(value.ToString());
				var addContent = testAssignmentService.AddContent(testAssignments,userId,testId);
				return JsonConvert.SerializeObject(addContent);
			}
			catch (Exception e)
			{
				resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
				return JsonConvert.SerializeObject(resultt);
			}
		}
	}
}