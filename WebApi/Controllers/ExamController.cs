using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Services;
using Model;

namespace WebApi.Controllers
{
    public class ExamController : Controller
    {
        private ExamServices examServices;

        public ExamController()
        {
            examServices = new ExamServices();
        }
        public string Index()
        {
            var result = examServices.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost]
        public string Index (Exam exam)
        {
            var result = examServices.Insert(exam);
            return JsonConvert.SerializeObject(result);
        }
    }
}