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
    public class UserController : ApiController
    {
        private UserSevices services;

        public UserController()
        {
            services = new UserSevices();
        }
        [HttpGet]
        [ValidateSSID(ActionId = 64)]
        // Get all User method
        public string GetUser()
        {
            ResultObject result = new ResultObject();
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result.Data = services.GetAll();
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
        [ValidateSSID(ActionId = 65)]
        // Get User by Id me thod
        public string GetUser(int userid)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = services.GetById(userid);
            return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
        }

        [HttpGet]
        [ValidateSSID(ActionId = 66)]
        // Get Detail User method
        public string GET(int id)
        {
            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
                var result = services.GetDetailUser(id);
                return JsonConvert.SerializeObject(result, Formatting.Indented, jsonSetting);
            }
        
        [HttpPost]
        [ValidateSSID(ActionId = 67)]
        //Add user method
        public string AddUser([FromBody] object value)
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
                    var user = JsonConvert.DeserializeObject<User>(value.ToString());
                    result.Success = services.Insert(user);
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
        [HttpPut]
        [ValidateSSID(ActionId = 65)]
        //Update user method
        public string Update(int id, [FromBody]object value)
        {
            if (value != null)
            {
                var user = JsonConvert.DeserializeObject<User>(value.ToString());
                user.UserId = id;
                var result = services.Update(user);
                return JsonConvert.SerializeObject(result);
            }
            return "FALSE";
        }
        [HttpDelete]
        [ValidateSSID(ActionId = 68)]
        //Delete user method
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

        [HttpGet]
        [ValidateSSID(ActionId = 64)]
        //Search user method
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

        [HttpPost]
        [ValidateSSID(ActionId = 64)]
        //Filter User method
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
                    var filterObject = JsonConvert.DeserializeObject<UserFilterModel>(value.ToString());
                    result.Data = services.FilterUser(filterObject);
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

        [HttpGet]
        [ValidateSSID(ActionId = 67)]
        //Check UserName
        public string CheckUserName(string userName)
        {
            return services.CheckUserName(userName).ToString();
        }
        // Get RoleName of user
        [HttpGet]
        [ValidateSSID(ActionId = 66)]
        public string GetRoleName(int idUser)
        {
            return services.GetRoleName(idUser);
        }

        [HttpGet]
        public string GetByUserId(string action,int id)
        {
            if ("GetUser".Equals(action))
            {
                var result = services.GetById(id);
                return JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                
            }

            return "true";
        }
    }
}