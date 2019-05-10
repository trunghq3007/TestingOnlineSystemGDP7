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
    public class TagController : Controller
    {
        private TagServices tagServices;

        public TagController()
        {
            tagServices = new TagServices();
        }

        public string Index()
        {
            var result = tagServices.GetAll().ToList();
           return JsonConvert.SerializeObject(result) ;      
        }

        [HttpPost]
        public string Index(Tag tag)
        {
            var result = tagServices.Insert(tag);
            return JsonConvert.SerializeObject(result);
        }

        //[HttpPut]
        //public string Index(Tag tag)
        //{
        //    var result = tagServices.Insert(tag);
        //    return JsonConvert.SerializeObject(result);
        //}

    }
}
