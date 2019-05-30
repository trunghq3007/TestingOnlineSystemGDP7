using Model;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        private QuestionServices questionServices;
        private TestService testServices;
        public TestController()
        {
            questionServices = new QuestionServices();
            testServices=new TestService();
        }
        
        [HttpPost]
        public string Post([FromBody] object value)
        {
            ResultObject resultt = new ResultObject();

            try
            {
                if (value != null && value.ToString().Count() > 0)
                {
                    var test = JsonConvert.DeserializeObject<Test>(value.ToString());
                    var result = testServices.Insert(test);
                    return JsonConvert.SerializeObject(result);

                }
                return "FALSE";
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }

           
        }

        [HttpGet]
        public string Getall()
        {
            ResultObject resultt = new ResultObject();
            try
            {
                var result = testServices.GetAll().ToList();
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
        
        [HttpGet]
		public string Get(int id)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                var result = testServices.getId(id);
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



		[HttpDelete]
		public string Put(int id)
		{
            ResultObject resultt = new ResultObject();

            try
            {
                var result = testServices.Delete(id);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
			
		}
	

		[HttpGet]
		public string Get(string searchString)
		{
            ResultObject resultt = new ResultObject();
            try
            {
                var result = testServices.SearchName(searchString).ToList();
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
           
		}

        [HttpPut]
        public string Put(int id, [FromBody] object value)
        {
            ResultObject resultt = new ResultObject();

            try
            {
                if (value != null)
                {
                    var test = JsonConvert.DeserializeObject<Test>(value.ToString());
                    test.Id = id;
                    var result = testServices.Update(test);
                    return JsonConvert.SerializeObject(result);
                }
                return "False";
            }
            catch (Exception e)
            {
                resultt.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(resultt);
            }
           
        }

	}
}