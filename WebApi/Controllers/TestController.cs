using Model;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowCrossSite]
    public class TestController : ApiController
    {
        private QuestionServices questionServices;
        private TestServices testServices;
        public TestController()
        {
            questionServices = new QuestionServices();
            testServices=new TestServices();
        }
        [HttpGet]
        public string Index()
        {
            var result = questionServices.GetAll();
            return JsonConvert.SerializeObject(result);
        }
        //[Route("Test")]
        //[System.Web.Http.HttpPost]
        //public string Index(Test test)
        //{
        //    var result = testServices.Insert(test);
        //    return JsonConvert.SerializeObject(result);

        //}
        [HttpPost]
        public string Post([FromBody] object value)
        {
            if (value != null && value.ToString().Count() > 0)
            {
                var test = JsonConvert.DeserializeObject<Test>(value.ToString());
                var result = testServices.Insert(test);
                return JsonConvert.SerializeObject(result);

            }
            return "FALSE";
        }

    }
}