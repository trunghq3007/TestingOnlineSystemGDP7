using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowCrossSite]
    public class TagController : ApiController
    {
        private TagServices service;

        public TagController()
        {
            service = new TagServices();
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
        public string Get([FromUri]string action, [FromBody]object value)
        {
            if (value != null )
            {
                if ("search".Equals(action))
                {
                    return JsonConvert.SerializeObject(service.Search(value.ToString()));
                }
            }
            var result = service.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Post([FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Tag>(value.ToString());
                var result = service.Insert(question);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id, [FromBody]object value)
        {
            if (value != null)
            {
                var question = JsonConvert.DeserializeObject<Tag>(value.ToString());
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
