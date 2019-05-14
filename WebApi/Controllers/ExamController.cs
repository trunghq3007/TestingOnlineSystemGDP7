using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Services;
using Model;
using System.Web.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApi.Controllers
{
    public class ExamController : ApiController
    {
        private ExamServices examServices;
        private QuestionServices questionServices;
        public ExamController()
        {
            examServices = new ExamServices();
            questionServices = new QuestionServices();
        }
        //get question
        [Route("Exams")]
        [System.Web.Http.HttpGet]
        public string Index()
        {
            var result = questionServices.GetAll();
            return JsonConvert.SerializeObject(result);
        }
        [Route("Exams")]
        [System.Web.Http.HttpPost]
        
        public string Index (Exam exam)
        {
            var result = examServices.Insert(exam);
            return JsonConvert.SerializeObject(result);
        }
    }
}