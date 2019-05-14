﻿using System;
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

    //[EnableCors("*", "*", "*")]

    [AllowCrossSite]

    public class ExamController : ApiController
    {
        private ExamServices services;
		private QuestionServices QuestionServices;
        public ExamController()
        {
            services = new ExamServices();
			QuestionServices = new QuestionServices();
        }

      
   
        [System.Web.Http.HttpGet]
        public string Index(int id)
        {
            var result = QuestionServices.GetById(2);
            return JsonConvert.SerializeObject(result);
        }



		[HttpGet]
		// GETID : User
		public string GetExam(int id)
		{


			var result = QuestionServices.GetById(id);


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
