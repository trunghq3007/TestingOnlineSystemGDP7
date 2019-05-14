using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Services;
namespace WebApi.Controllers
{
    public class UserGroupController : ApiController
    {
        private UserGroupServices services;

        public UserGroupController()
        {
            services = new UserGroupServices();
        }
        // GET: UserGroup
        public string GetUserInGroup(int id)
        {
            var result = services.GetUserInGroup(1);
            return JsonConvert.SerializeObject(result);
        }
        [System.Web.Http.HttpDelete]
        public string Put(int iduser, int idgroup)
        {
            var result = services.DeleteUserGroup(iduser,idgroup);
            return JsonConvert.SerializeObject(result);
        }
    }
}