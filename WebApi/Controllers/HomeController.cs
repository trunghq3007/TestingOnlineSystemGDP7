using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Model;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private GroupServices groupServices;
        public HomeController()
        {
           // groupServices = new GroupServices();
        }
        public ActionResult Index(Group group)
        {          
            //groupServices.Insert(group);           
            return View();
        }
    }
}
