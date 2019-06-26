using Model;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Commons;
using Model.ViewModel;
using System.Dynamic;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private UserSevices services;

        public LoginController()
        {
            services = new UserSevices();
        }
        [HttpPost]
        public ResultObject Login([FromBody] object value)
        {

            var jsonSetting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var result = new ResultObject();
            try
            {
                var model = JsonConvert.DeserializeObject<LoginModel>(value.ToString());
                result.Success = services.Login(model, true);
                if (result.Success == 1)
                {
                    var user = services.GetByUsername(model.UserName);
                    var listAction = services.GetListAction(user.UserName);

                    dynamic data = new ExpandoObject();
                    data.ListAction = listAction;

                    data.Name = user.UserId;
                    result.Data = JsonConvert.SerializeObject(data);
                    return result;
                }
                return result;
            }
            catch (Exception e)
            {
                result.Message = "EXCEPTION: " + e.Message + "Stack: " + e.StackTrace;
                return result;
            }
        }
    }
}