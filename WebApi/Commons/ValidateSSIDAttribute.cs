using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Model.ViewModel;
using Newtonsoft.Json;

namespace WebApi.Commons
{
    public class ValidateSSIDAttribute : AuthorizeAttribute
    {
        public int ActionId { get; set; }
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            var keys = HttpContext.Current.Request.Headers.GetValues("Permission");
            if (keys == null || keys.Length < 0) return false;
            else
            {
                dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(keys[0]);
                long userId = data.Name;


                //List<int> listAction = JsonConvert.DeserializeObject<List<int>>(val.ToString());
                //string UserID = data.Name;

                foreach (var item in data.ListAction)
                {
                    if (item == ActionId) return true;
                }
            }
            return false;

            //var session = (UserLogin)HttpContext.Current.Session[Commons.CommonConstants.USER_SESSION];
            //if (session == null)
            //{
            //    return false;
            //}
            //List<int> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); // Call another method to get rights of the user from DB

            //if (privilegeLevels.Contains(this.ActionId))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}