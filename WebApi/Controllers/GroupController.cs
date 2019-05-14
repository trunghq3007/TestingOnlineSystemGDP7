using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Services;

namespace WebApi.Controllers
{
    public class GroupController : ApiController
    {
        private GroupServices services;

        public GroupController()
        {
            services = new GroupServices();
        }
        // GET: Group
        [HttpGet]
        public string Get()
        {
            var result = services.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }
    }
}