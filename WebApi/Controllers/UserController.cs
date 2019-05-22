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
        [HttpGet]
        // GETID : User
        public string GetUser(int id)
        {
            var result = services.GetById(id);
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost]
        public string AddUser([FromBody] object value)
        {
            if (value != null)
            {
                var user = JsonConvert.DeserializeObject<User>(value.ToString());

                var result = services.Insert(user);
                return JsonConvert.SerializeObject(result);

            }
            return "FALSE";
        }
        [HttpPut]
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
        public string Delete(int id)
        {
            var result = services.Delete(id);
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet]
        public string Get(string searchString)
        {
            var result = services.Search(searchString).ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string Get([FromUri]string action, [FromBody] object value)
        {
            if (value != null)
            {
                if ("filter".Equals(action))
                {
                    try
                    {
                        var filterObject = JsonConvert.DeserializeObject<UserFilterModel>(value.ToString());
                        return JsonConvert.SerializeObject(services.FilterUser(filterObject));
                    }
                    catch (Exception)
                    {
                        return "Object fillter not convert valid";
                    }
                }
            }
            var result = services.GetAll().ToList();
            return JsonConvert.SerializeObject(result);
        }

    }
}