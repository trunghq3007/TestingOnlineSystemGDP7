using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Services;

using Model;
namespace WebApi.Controllers
{
    public class SemesterExamUserController : ApiController
    {
        private SemesterExamUserServices service;
        // GET: SemesterExamUser
        public SemesterExamUserController()
        {
            service = new SemesterExamUserServices();
        }
        [System.Web.Http.HttpGet]
        public string Get(int id)
        {
            var result = service.candidates(id);
            //string a = "";
            //foreach (SemesterExam_User item in result)
            //{
            //     a = item.SemesterExam.SemesterName;
            //}
            //string b = a;
            
            return JsonConvert.SerializeObject(result);
        }
    }
}