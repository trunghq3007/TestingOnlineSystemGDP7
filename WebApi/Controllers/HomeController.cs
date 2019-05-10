using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new DBEntityContext();
            db.Tags.Add(new Model.Tag { Name = "tag", Status = 0, Description = "test" });
            db.SaveChanges();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
