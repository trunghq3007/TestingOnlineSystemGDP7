using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Services;
using Model;
using System.Web.Http.Cors;
using WebApi.Commons;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GroupController : ApiController
    {
        private GroupServices services;

        public GroupController()
        {
            services = new GroupServices();
        }
        // GET: Group
        [HttpGet]
        //[HasCredential(ActionId = 11)]
        [ValidateSSID(ActionId =11)]
        public string Get()
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            ResultObject result = new ResultObject();
            try
            {
                result.Data = services.GetAll().ToList();
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }

        }
        //Add new Group method
        [HttpPost]
        [ValidateSSID(ActionId = 12)]
        public string Post([FromBody]object value)
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
                    var group = JsonConvert.DeserializeObject<Group>(value.ToString());
                    result.Success = services.Insert(group);
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
        //Get Group by Id method
        [HttpGet]
        [ValidateSSID(ActionId = 13)]
        public string Get(int id)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetById(id);
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }

        }
        //Search Group method
        [HttpGet]
        [ValidateSSID(ActionId = 11)]
        public string Get(string searchString)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.Search(searchString).ToList();
                if (result.Data != null) result.Success = 1;
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }

        }
        //Delete Group method
        [HttpDelete]
        [ValidateSSID(ActionId = 14)]
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
        //Filter Group method
        [HttpPost]
        [ValidateSSID(ActionId = 11)]
        public string Get([FromUri]string action, [FromBody] object value)
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
                    result.Data = services.FilterGroup(filterObject);
                    if (result.Data != null) result.Success = 1;
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
                result.Data = services.GetAll().ToList();
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }
        }
        //Update Group method
        [HttpPut]
        [ValidateSSID(ActionId = 15)]
        public string Put(int id, string groupname)
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                if (groupname != null)
                {
                    result.Success = services.Update(id, groupname);
                    return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
                }
                return "False";
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return JsonConvert.SerializeObject(result);
            }           
        }
        [HttpGet]
        [ValidateSSID(ActionId = 15)]
        public string CheckNameGroup(string groupName)
        {
            return services.CheckNameGroup(groupName).ToString();
        }
    }
}
