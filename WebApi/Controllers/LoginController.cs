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
            var model = JsonConvert.DeserializeObject<LoginModel>(value.ToString());
            var result = new ResultObject();
            result.Success = services.Login(model, true);
            if (result.Success == 1)
            {
                var user = services.GetByUsername(model.UserName);
                var listAction = services.GetListAction(user.UserName);

                //var userSession = new UserLogin();
                //userSession.UserName = user.UserName;
                //userSession.UserId = user.UserId;
                //userSession.RoleId = user.RoleId;
                //var listRoleActions = services.GetListAction(model.UserName);
                //var userContext = new UserContext { CurrentUser = user, ListActionId = listAction.ToList() };
                //HttpContext.Current.Session[HttpContext.Current.Session.SessionID] = userContext;

                //HttpContext.Current.Session.Add(Commons.CommonConstants.SESSION_CREDENTIALS, listRoleActions);
                //HttpContext.Current.Session.Add(Commons.CommonConstants.USER_SESSION, userSession);
                result.Data = JsonConvert.SerializeObject(listAction);
                return result;
            }
            return result;
        }
    }
}