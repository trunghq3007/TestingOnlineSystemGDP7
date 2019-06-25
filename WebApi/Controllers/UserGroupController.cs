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
    
    public class UserGroupController : ApiController
    {
        private UserGroupServices services;

        public UserGroupController()
        {
            services = new UserGroupServices();
        }
        [HttpGet]
        [ValidateSSID(ActionId = 69)]
        // Get User In Group method
        public string GetUserInGroup(int id)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetUserInGroup(id);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        [HttpGet]
        [ValidateSSID(ActionId = 69)]
        // Search User In Group method
        public string Get(int id, string searchString)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.SearchUserInGroup(id, searchString);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }

        }
        //Delete User In Group
        [HttpDelete]
        [ValidateSSID(ActionId = 70)]
        public string Delete(int iduser, int idgroup)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Success = services.DeleteUserGroup(iduser, idgroup);
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Filter User In Group
        [HttpPost]
        [ValidateSSID(ActionId = 71)]
        public string Get([FromUri]string action, [FromBody] object value, int id)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (value == null)
            {
                result.Message = "Data null";
                return JsonConvert.SerializeObject(result);
            }

            try
            {
                if ("filter".Equals(action))
                {
                    var filterObject = JsonConvert.DeserializeObject<GroupFilterModel>(value.ToString());
                    result.Data = services.FilterUserInGroup(filterObject,id);
                    if (result.Data != null) result.Success = 1;
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
                result.Data = services.GetUserInGroup(id).ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Add User Into Group
        [HttpPost]
        //[AllowAnonymous]
        [ValidateSSID(ActionId = 72)]
        public string InsertUserGroup([FromBody]object  userGroup)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                var userGroupp = JsonConvert.DeserializeObject<List<UserGroup>>(userGroup.ToString());
               
                result.Success = services.InsertUserGroup(userGroupp);
                return JsonConvert.SerializeObject(result);

            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Get User not add to Group
        [HttpGet]
        [ValidateSSID(ActionId = 73)]
        public string GetUserOutGroup(int idgroup)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetUserOutGroup(idgroup);
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