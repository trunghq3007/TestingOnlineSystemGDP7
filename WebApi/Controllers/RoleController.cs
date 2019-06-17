using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using Newtonsoft.Json;
using Services;
using WebApi.Commons;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private RoleServices services;
        public RoleController()
        {
            services = new RoleServices();
        }
        [HttpGet]
        [ValidateSSID(ActionId = 20)]
        // GET: Role
        public string GetRole()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = services.GetAll();
            return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
        }
        [HttpDelete]
        [ValidateSSID(ActionId = 21)]
        public string Delete(int id)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Success = services.Delete(id);
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }

        }
        
        [HttpPost]
        [ValidateSSID(ActionId = 22)]
        //Add user method
        public string AddRole([FromBody] object value)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                if (value != null)
                {
                    var role = JsonConvert.DeserializeObject<Role>(value.ToString());
                    result.Success = services.Insert(role);
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
                else
                {
                    result.Message = "Null content";
                    return JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }

    }
}
