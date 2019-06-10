using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Commons
{
    public class HasCredentialAttribute: AuthorizeAttribute
    {
        public int ActionId { get; set; }
        protected override bool IsAuthorized(HttpActionContext httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[Commons.CommonConstants.USER_SESSION];
            if (session == null)
            {
                return false;
            }
            List<int> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName); // Call another method to get rights of the user from DB

            if (privilegeLevels.Contains(this.ActionId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private List<int> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<int>)HttpContext.Current.Session[Commons.CommonConstants.SESSION_CREDENTIALS];
            return credentials;
        }
    }
}