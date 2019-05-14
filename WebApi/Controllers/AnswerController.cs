﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AnswerController : ApiController
    {
        private AnswerServices service;

        public AnswerController()
        {
            service = new AnswerServices();
        }
        [HttpGet]
        public string Get()
        {
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(int id)
        {
            var result = service.GetById(id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        public string Get([FromUri]string action, [FromBody]string value)
        {
            if (value != null && !"".Equals(value))
            {
                if ("search".Equals(action))
                {
                    return JsonConvert.SerializeObject(service.Search(value));
                }
            }
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            if (value.Count() > 0)
            {
                var question = JsonConvert.DeserializeObject<Answer>(value);
                var result = service.Insert(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id, [FromBody]string value)
        {
            if (value.Count() > 0)
            {
                var question = JsonConvert.DeserializeObject<Answer>(value);
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
