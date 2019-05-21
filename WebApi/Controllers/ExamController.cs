using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Services;
using Model;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{


    [AllowCrossSite]
    public class ExamController : ApiController
    {
        private ExamServices services;
		private QuestionServices QuestionServices;
        private ExamQuestionServices examQuestion;
        public ExamController()
        {
            services = new ExamServices();
			QuestionServices = new QuestionServices();
            examQuestion=new ExamQuestionServices();
        }

      
   
        [HttpGet]
        public string Index()
        {
            var result = services.GetAll();
            return JsonConvert.SerializeObject(result);
        }



		[HttpGet]
		// GETID : User
		public string GetExam(int id)
		{


			var result = examQuestion.GetListQuestionById(id);


			return JsonConvert.SerializeObject(result);
		}


        [HttpGet]
        public string Get([FromUri]string action, [FromBody]object value)
        {
            if (value != null)
            {
                if ("search".Equals(action))
                {
                    return JsonConvert.SerializeObject(QuestionServices.Search(value.ToString()));
                }
                if ("fillter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<QuestionFillterModel>(value.ToString());
                        return JsonConvert.SerializeObject(QuestionServices.Filter(filterObject));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = QuestionServices.GetAll().ToList();
            return JsonConvert.SerializeObject(result);

        }


        [HttpPost]
        public string InsertExam([FromBody] string value)
        {
            if (value.Count() > 0)
            {
                var exam = JsonConvert.DeserializeObject<Exam>(value);
                var result = services.Insert(exam);
                return JsonConvert.SerializeObject(result);

            }
            return "FALSE";
        }
        [HttpPut]
        public string Update(int id, [FromBody]string value)
        {
            if (value.Count() > 0)
            {
                var exam = JsonConvert.DeserializeObject<Exam>(value);
                exam.Id = id;
                var result = services.Update(exam);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        public string Delete(int id)
        {
            var result = services.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
    }
}
