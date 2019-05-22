using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Newtonsoft.Json;
using Services;

namespace WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryService service;

        public CategoryController()
        {
            service = new CategoryService();
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
            if (value != null)
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
                var category = JsonConvert.DeserializeObject<Category>(value.ToString());
                category.CreatedBy = "anonymous user";
                category.CreatedDate = DateTime.Now;
                var result = service.Insert(category);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id, [FromBody]object value)
        {
            if (value != null)
            {
                var category = JsonConvert.DeserializeObject<Category>(value.ToString());
                category.Id = id;
                category.CreatedBy = "anonymous user";
                category.CreatedDate = DateTime.Now;
                var result = service.Update(category);
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
