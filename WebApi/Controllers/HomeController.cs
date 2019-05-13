using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Services;
using Model;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private SemesterExamServices semesterExamServices;
        public HomeController()
        {
            semesterExamServices = new SemesterExamServices();
        }
        public ActionResult Index()
        {
            SemesterExam semesterExam = new SemesterExam("dd", DateTime.Now, DateTime.Now, "code1", 1);
            semesterExamServices.Insert(semesterExam);           
            return View();
        }
    }
}
