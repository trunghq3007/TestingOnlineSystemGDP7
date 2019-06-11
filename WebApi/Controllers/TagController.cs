using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using Model;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            // return JsonConvert.SerializeObject(result);
            return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
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
                var question = JsonConvert.DeserializeObject<Tag>(value);
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
                var question = JsonConvert.DeserializeObject<Tag>(value);
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
