using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Model;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class QuestionController : Controller
    {
        private QuestionServices service;

        public QuestionController()
        {
            service = new QuestionServices();
        }

        public string Index()
        {
            var result = service.GetAll().ToList();
           return JsonConvert.SerializeObject(result) ;      
        }

        [HttpPost]
        public string Index(Question question)
        {
            var result = service.Insert(question);
            return JsonConvert.SerializeObject(result);
        }

        //[HttpPut]
        //public string Index(Question tag)
        //{
        //    var result = tagServices.Insert(tag);
        //    return JsonConvert.SerializeObject(result);
        //}

    }
}
