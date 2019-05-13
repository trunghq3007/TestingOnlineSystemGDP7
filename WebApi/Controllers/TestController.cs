using Model;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class TestController : Controller
    {
        private QuestionServices service;

        public TestController()
        {
            service = new QuestionServices();
        }

        public string Index()
        {
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Index(Question question)
        {
            var result = service.Insert(question);
            return JsonConvert.SerializeObject(result);
        }

    }
}