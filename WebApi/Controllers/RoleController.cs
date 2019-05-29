using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Model;
using Newtonsoft.Json;
using Services;

namespace WebApi.Controllers
{
    public class RoleController : ApiController
    {
        private RoleServices sevices;
        public RoleController()
        {
            sevices = new RoleServices();
        }
        [HttpGet]
        // GET: Role
        public string GetRole()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = sevices.GetAll();
            return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
        }

    }
}
