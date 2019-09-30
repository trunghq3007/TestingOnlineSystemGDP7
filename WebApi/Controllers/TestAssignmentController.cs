using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json;
using Services;

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
	}
}