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
    public class RoleActionController : ApiController
    {
        private RoleActionServices services;

        public RoleActionController()
        {
            services = new RoleActionServices();
        }
        [HttpGet]
        [ValidateSSID(ActionId = 23)]
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
        //Delete Action in Role
        [HttpDelete]
        [ValidateSSID(ActionId = 24)]
        public string Delete(int idAction, int idRole)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Success = services.DeleteActionRole(idAction, idRole);
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Insert Action to Role
        [HttpPost]
        [ValidateSSID(ActionId = 25)]
        public string InsertRoleAction([FromBody]object roleAction)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                var roleActionn = JsonConvert.DeserializeObject<RoleAction>(roleAction.ToString());
                result.Success = services.InsertRoleAction(roleActionn);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Get Action not in Role
        [HttpGet]
        [ValidateSSID(ActionId = 26)]
        public string GetActionOutRole(int idRole)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetActionOutRole(idRole);
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