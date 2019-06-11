using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using Newtonsoft.Json;
using Services;
namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleActionController : ApiController
    {
        private RoleActionServices services;

        public RoleActionController()
        {
            services = new RoleActionServices();
        }
        [HttpGet]
        // Get User In Group method
        public string GetActionInRole(int roleId)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetActionInRole(roleId);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}