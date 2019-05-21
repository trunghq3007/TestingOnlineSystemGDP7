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
    [AllowCrossSite]
    public class UserGroupController : ApiController
    {      
        private UserGroupServices services;
        
        public UserGroupController()
        {
            services = new UserGroupServices();
        }
        [HttpGet]
        // GET: UserGroup
        public string GetUserInGroup(int id)
        {
            var result = services.GetUserInGroup(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpGet]
        // GET: UserGroup
        public string Get(int id,string searchString)
        {
            var result = services.SearchUserInGroup(id,searchString);
            return JsonConvert.SerializeObject(result);
        }
        [HttpDelete]
        public string Delete(int iduser, int idgroup)
        {
            var result = services.DeleteUserGroup(iduser,idgroup);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Get([FromUri]string action, [FromBody] object value,int id)
        {
            if (value != null)
            {
                if ("filter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<GroupFilterModel>(value.ToString());
                        return JsonConvert.SerializeObject(services.FilterUserInGroup(filterObject,id));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = services.GetUserInGroup(id).ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string InsertUserGroup(int iduser, int idgroup)
        {
            var result = services.InsertUserGroup(iduser, idgroup);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        public string GetUserOutGroup(int idgroup)
        {
            var result = services.GetUserOutGroup(idgroup);
            return JsonConvert.SerializeObject(result);
        }
    }
}