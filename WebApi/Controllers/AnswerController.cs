using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApi.Controllers 
{
    
    public class AnswerController : ApiController
    {
        private AnswerServices service;

        public AnswerController()
        {
            service = new AnswerServices();
        }
        [Route("Answers")]
        [System.Web.Http.HttpGet]
        public string GetAll()
        {
            var result = service.GetAll().ToList();
           return JsonConvert.SerializeObject(result) ;      
        }
        [Route("Answers")]
        [System.Web.Http.HttpPost]
        public string Insert(Answer question)
        {
            var result = service.Insert(question);
            return JsonConvert.SerializeObject(result);
        }

        [Route("Answers/{id}")]
        [System.Web.Http.HttpPut]
        public string Update(Answer question,int id)
        {
            question.Id = id;
            var result = service.Update(question);
            return JsonConvert.SerializeObject(result);
        }

        [Route("Answers/{id}")]
        [System.Web.Http.HttpPut]
        public string Delete(int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }

    }
}
