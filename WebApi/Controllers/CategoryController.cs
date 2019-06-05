﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
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

        [HttpPost]
        public string Get([FromUri]string action, [FromBody]string value)
        {
            if (value != null && !"".Equals(value))
            {
                if ("search".Equals(action))
                {
                    return JsonConvert.SerializeObject(service.Search(value));
                }
                if ("delete".Equals(action))
                {
                    return JsonConvert.SerializeObject(service.DeleteBatch(value));
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
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = "Auto User";
                var result = service.Insert(category);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }

        [HttpPut]
        public string Put(int id, [FromBody]object value)
        {
            if (value != null )
            {
                var category = JsonConvert.DeserializeObject<Category>(value.ToString());
                category.Id = id;
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = "Auto User";
                var result = service.Update(category);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        public string Put(int id)
        {
            try
            {
                var result = service.Delete(id);
                return JsonConvert.SerializeObject(result);
            }
           catch
            {
                return "0";
            }
        }
    }
}
