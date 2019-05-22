using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Services;
using Model;
 

namespace WebApi.Controllers
{
    [AllowCrossSite]
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
        [HttpPost]
        public string Post([FromBody]object value)
        {
            if (value!= null)
            {
                var group = JsonConvert.DeserializeObject<Group>(value.ToString());
                var result = services.Insert(group);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpGet]
        public string Get(int id)
        {
            var result = services.GetById(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        public string Get(string searchString)
        {
            var result = services.Search(searchString).ToList();
            return JsonConvert.SerializeObject(result);
        }
        [HttpDelete]
        public string Delete(int id)
        {
            var result = services.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
         
        [HttpPost]
        public string Get([FromUri]string action, [FromBody] object value)
        {
            if (value != null)
            {
                if ("filter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<GroupFilterModel>(value.ToString());                       
                        return JsonConvert.SerializeObject(services.FilterGroup(filterObject));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }               
            }
            var result = services.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }
    }
}