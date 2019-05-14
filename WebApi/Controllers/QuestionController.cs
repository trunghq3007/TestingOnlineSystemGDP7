using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Model;

using Newtonsoft.Json;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    public class QuestionController : ApiController
    {
        private QuestionServices service;

        public QuestionController()
        {
            service = new QuestionServices();
        }
        [HttpGet]
        public string Get()
        {
            var result = service.GetAll().ToList();
           return JsonConvert.SerializeObject(result) ;      
        }
        [HttpGet]
        public string Get(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            if (value.Count() > 0)
            {
                var question = JsonConvert.DeserializeObject<Question>(value);
                var result = service.Insert(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id ,[FromBody]string value)
        {
            if (value.Count() > 0)
            {
                var question = JsonConvert.DeserializeObject<Question>(value);
                question.Id = id;
                var result = service.Update(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        public string Put(int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }


    }
}
