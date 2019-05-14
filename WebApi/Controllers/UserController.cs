using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Services;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private UserSevices services;

        public UserController()
        {
            services = new UserSevices();
        }
        [HttpGet]
        // GET: User
        public string GetUser()
        {
            var list = services.GetAll();
            return JsonConvert.SerializeObject(list);
        }

    }
}