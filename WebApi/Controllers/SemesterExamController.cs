using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Newtonsoft.Json;
using Services;

namespace WebApi.Controllers
{
    public class SemesterExamController : Controller
    {
        private SemesterExamServices service;
        public SemesterExamController()
        {
            service = new SemesterExamServices();
        }

        // GET: SemesterExam
        [Route("SemesterExams")]
        [System.Web.Http.HttpGet]
        public string GetAll()
        {
            var result = service.GetAll();
            return JsonConvert.SerializeObject(result);
        }

        [Route("SemesterExams")]
        [System.Web.Http.HttpPost]
        public string Insert(SemesterExam semesterExam)
        {
            var result = service.Insert(semesterExam);
            return JsonConvert.SerializeObject(result);
        }

        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpPut]
        public string Update(SemesterExam semesterExam, int id)
        {
            semesterExam.ID = id;
            var result = service.Update(semesterExam);
            return JsonConvert.SerializeObject(result);
        }
        [Route("SemesterExam/{id}")]
        [System.Web.Http.HttpPut]
        public string Delete( int id)
        {
            var result = service.Delete(id);
            return JsonConvert.SerializeObject(result);
        }
    }
}